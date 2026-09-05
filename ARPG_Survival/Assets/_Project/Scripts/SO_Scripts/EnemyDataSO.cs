using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemyData", menuName = "ARPG/Enemy Data", order = 1)]
public class EnemyDataSO : ScriptableObject
{
    [Header("Base Stats")]
    [Tooltip("Punti Vita massimi")]
    [SerializeField] private float _maxHealth;

    [Tooltip("Attacco del nemico")]
    [SerializeField] private float _atk;

    [Tooltip("Valore di Difesa")]
    [SerializeField] private float _def;

    [Header("Identity")]
    [Tooltip("Nome del nemico")]
    [SerializeField] private string _enemyName;

    public float MaxHealth => _maxHealth;
    public float Atk => _atk;
    public float Def => _def;
    public string EnemyName => _enemyName;
}
