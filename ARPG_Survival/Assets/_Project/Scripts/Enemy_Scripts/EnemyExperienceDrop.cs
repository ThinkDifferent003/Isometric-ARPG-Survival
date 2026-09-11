using UnityEngine;

public class EnemyExperienceDrop : MonoBehaviour
{
    [Header("Enemy Data")]
    [SerializeField] private EnemyDataSO _enemyData;

    [Header("Level & XP Settings")]
    [SerializeField] private int _enemyLevel;
    [SerializeField] private float _xpMultiplierPerLevel;
    private float _baseXpReward;
    

    [Header("Drop Prefab")]
    [SerializeField] private GameObject _xpOrbPrefab;


    private void Awake()
    {
        if (_enemyData != null) _baseXpReward = _enemyData.BaseXpReward;
    }
    public void DropExperience()
    {
        if (_xpOrbPrefab == null) return;
        float totalXp = _baseXpReward * Mathf.Pow(_xpMultiplierPerLevel, _enemyLevel - 1);
        GameObject orbInstance = Instantiate(_xpOrbPrefab, transform.position + Vector3.up * 0.5f, Quaternion.identity);
        if (orbInstance.TryGetComponent<ExperienceOrb>(out var xpOrb)) xpOrb.SetXpValue(totalXp);
    }
}
