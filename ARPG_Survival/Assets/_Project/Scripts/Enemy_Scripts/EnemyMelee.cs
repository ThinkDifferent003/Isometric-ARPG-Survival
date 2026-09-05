using System.Collections;
using UnityEngine;

public class EnemyMelee : EnemyAI
{
    [Header("Melee Attack Settings")]
    [SerializeField] private MeleeHitbox _atkHitbox;
    [SerializeField] private float _atkCooldown;
    [SerializeField] private float _atkActiveDuration;

    protected override void Start()
    {
        base.Start();
        if (_atkHitbox != null) _atkHitbox.gameObject.SetActive(false);
    }
    protected override void ExecuteBehavior(float distToPlayer)
    {
        if (distToPlayer <= _atkRange)
        {
            _agent.isStopped = true;
            RotateTowardsPlayer();
            if (Time.time >= _lastAttackTime + _atkCooldown) StartCoroutine(PerformMeleeAttack());
        }
        else
        {
            _agent.isStopped = false;
            _agent.SetDestination(_playerTransform.position);
        }
    }
    private IEnumerator PerformMeleeAttack()
    {
        _lastAttackTime = Time.time;
        Debug.Log($"<color=orange>[MeleeEnemyAI] {gameObject.name} sta INIZIANDO l'attacco!</color>");
        if (_atkHitbox != null)
        {
            float atkDmg = _enemyData != null ? _enemyData.Atk : 10f;
            _atkHitbox.Inizialize(atkDmg);

            _atkHitbox.gameObject.SetActive(true);
            Debug.Log("<color=green>[MeleeEnemyAI] Hitbox ATTIVATA</color>");
            yield return new WaitForSeconds(_atkActiveDuration);
            _atkHitbox.gameObject.SetActive(false);
            Debug.Log("<color=grey>[MeleeEnemyAI] Hitbox DISATTIVATA</color>");
        }
    }
}
