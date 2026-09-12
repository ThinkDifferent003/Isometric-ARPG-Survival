using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Spwn Settings")]
    [SerializeField] private GameObject[] _enemyPrefabs;

    public void SpawnEnemy(int currentLevel)
    {
        if (_enemyPrefabs == null || _enemyPrefabs.Length == 0) return;
        GameObject randomPrefab = _enemyPrefabs[Random.Range(0, _enemyPrefabs.Length)];
        GameObject spawnedEnemy = Instantiate(randomPrefab, transform.position, transform.rotation);
        if (spawnedEnemy.TryGetComponent<EnemyExperienceDrop>(out var xpDrop)) xpDrop.SetEnemyLevel(currentLevel);
    }
}
