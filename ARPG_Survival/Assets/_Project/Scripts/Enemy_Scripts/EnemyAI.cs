using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public abstract class EnemyAI : MonoBehaviour
{
    [Header("Base AI Settings")]
    [SerializeField] protected EnemyDataSO _enemyData;
    [SerializeField] protected float _atkRange;

    protected NavMeshAgent _agent;
    protected Transform _playerTransform;
    protected float _lastAttackTime;

    protected virtual void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }
    protected virtual void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null) _playerTransform = playerObj.transform;

        if (_enemyData != null) _agent.stoppingDistance = _atkRange;
    }
    protected virtual void Update()
    {
        if (_playerTransform == null) return;
        float distToPlayer = Vector3.Distance(transform.position, _playerTransform.position);
        ExecuteBehavior(distToPlayer);
    }

    /// <summary>
    /// Metodo che verrà sovrascritto da Melee e ranged
    /// </summary>
    protected abstract void ExecuteBehavior(float distToPlayer);
    protected void RotateTowardsPlayer()
    {
        Vector3 dir = (_playerTransform.position - transform.position).normalized;
        dir.y = 0;
        if (dir != Vector3.zero)
        {
            Quaternion targetRot = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, Time.deltaTime * 10f);
        }
    }
}
