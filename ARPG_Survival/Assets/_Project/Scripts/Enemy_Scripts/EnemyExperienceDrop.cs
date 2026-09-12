using UnityEngine;

public class EnemyExperienceDrop : MonoBehaviour
{
    [Header("Enemy Data")]
    [SerializeField] private EnemyDataSO _enemyData;

    [Header("Level & XP Settings")]
    [SerializeField] private int _enemyLevel;
    [SerializeField] private float _xpMultiplierPerLevel;
    private float _baseXpReward;

    private void Awake()
    {
        if (_enemyData != null) _baseXpReward = _enemyData.BaseXpReward;
    }
    public void DropExperience()
    {
        float totalXp = _baseXpReward * Mathf.Pow(_xpMultiplierPerLevel, _enemyLevel - 1);
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null && player.TryGetComponent<PlayerLevelSystem>(out var playerLevel)) playerLevel.AddExperience(totalXp);
    }
    public void SetEnemyLevel(int lvl)
    {
        _enemyLevel = lvl;
    }
}
