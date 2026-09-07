using UnityEngine;

public class EnemyRanged : EnemyAI
{
    [Header("Ranged IA Settings")]
    [SerializeField] private float _preferredDistance;
    [SerializeField] private float _strafeSpeed;
    [SerializeField] private float _attackCooldown;

    [Header("Ranged Attack Settings")]
    [SerializeField] private Transform _firePoint;
    [SerializeField] private float _projectileSpeed;

    private Vector3 _currentStrafeDirection;
    private float _nextStrafeChangeTime;

    protected override void ExecuteBehavior(float distToPlayer)
    {
        RotateTowardsPlayer();
        HandleMovement(distToPlayer);
        if (Time.time >= _lastAttackTime + _attackCooldown) ShootProjectile();
    }
    private void HandleMovement(float distToPlayer)
    {
        if (distToPlayer < _preferredDistance - 1.5f)
        {
            Vector3 retreatDir = (transform.position - _playerTransform.position).normalized;
            Vector3 retreatTarget = transform.position + retreatDir * 2f;
            _agent.isStopped = false;
            _agent.SetDestination(retreatTarget);
        }
        else if (distToPlayer > _preferredDistance + 2f)
        {
            _agent.isStopped = false;
            _agent.SetDestination(_playerTransform.position);
        }
        else
        {
            _agent.isStopped = true;
            if (Time.time >= _nextStrafeChangeTime)
            {
                _currentStrafeDirection = (Random.value > 0.5f) ? transform.right : - transform.right;
                _nextStrafeChangeTime = Time.time + Random.Range(1.5f, 3f);
            }
            Vector3 strafeTarget = transform.position + _currentStrafeDirection * (_strafeSpeed * Time.deltaTime);
            _agent.Move(_currentStrafeDirection * (_strafeSpeed * Time.deltaTime));
        }
    }
    private void ShootProjectile()
    {
        _lastAttackTime = Time.time;
        if (_firePoint == null) return;
        GameObject projObj = ObjectPool.Instance.Get();
        projObj.transform.position = _firePoint.position;
        projObj.transform.rotation = _firePoint.rotation;
        if (projObj.TryGetComponent<Projectile>(out var projectile))
        {
            float atkDmg = _enemyData != null ? _enemyData.Atk : 5f;
            projectile.Initialize(atkDmg, "Enemy");
            Debug.Log($"<color=cyan>[RangedEnemyAI] {gameObject.name} ha sparato un proiettile ({atkDmg} danni)!</color>");
        }
    }
}
