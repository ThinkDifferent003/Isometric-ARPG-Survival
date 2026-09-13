using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public static WaveManager Instance { get; private set; }

    [Header("References")]
    [SerializeField] private PlayerLevelSystem _playerLevelSystem;
    [SerializeField] private EnemySpawner[] _spawners;

    [Header("Spawn Rhythm Settings")]
    [SerializeField] private float _initialSpawnInterval;
    [SerializeField] private float _minimunSpawnInterval;
    [SerializeField] private float _rhythmIncreaseRate;

    [Header("Current Status")]
    [SerializeField] private float _currentSpawnInterval;
    [SerializeField] private int _currentEnemyLevel;

    private float _spawnTimer;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy (gameObject);
        if (_playerLevelSystem == null) _playerLevelSystem = FindFirstObjectByType<PlayerLevelSystem>();
        if (_spawners == null || _spawners.Length == 0) _spawners = FindObjectsByType<EnemySpawner>(FindObjectsSortMode.None);
        _currentSpawnInterval = _initialSpawnInterval;
    }
    private void Update()
    {
        UpdateEnemyLevel();
        UpdateSpawnRhythm();
        HandleSpawning();
    }
    private void UpdateEnemyLevel()
    {
        if (_playerLevelSystem != null) _currentEnemyLevel = _playerLevelSystem.CurrentLevel;
    }
    private void UpdateSpawnRhythm()
    {
        if (_currentSpawnInterval > _minimunSpawnInterval)
        {
            _currentSpawnInterval -= _rhythmIncreaseRate * Time.deltaTime;
            _currentSpawnInterval = Mathf.Max(_currentSpawnInterval, _minimunSpawnInterval);
        }
    }
    private void HandleSpawning()
    {
        _spawnTimer += Time.deltaTime;
        if (_spawnTimer >= _currentSpawnInterval)
        {
            _spawnTimer = 0;
            TriggerRandomSpawn();
        }
    }
    private void TriggerRandomSpawn()
    {
        if (_spawners == null || _spawners.Length == 0) return;
        int randomIndex = Random.Range(0, _spawners.Length);
        _spawners[randomIndex].SpawnEnemy(_currentEnemyLevel);
    }
}
