using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private EnemyDataSO _enemyData;
    public float CurrentHealth { get; private set; }

    private void Awake()
    {
        if (_enemyData != null) CurrentHealth = _enemyData.MaxHealth;
    }
    public void TakeDamage(float dmg)
    {
        if (CurrentHealth <= 0) return;

        float def = _enemyData != null ? _enemyData.Def : 0f;
        float finalDmg = Mathf.Max(dmg - def, 1f);
        CurrentHealth -= finalDmg;
        Debug.Log($"<color=red>[Enemy] {gameObject.name} ha subito {finalDmg} danni! Vita rimanente: {CurrentHealth}/{_enemyData.MaxHealth}</color>");
        if (CurrentHealth <= 0) Die();
    }
    private void Die()
    {
        Debug.Log($"<color=black>[Enemy] {gameObject.name} è stato sconfitto!</color>");
    }
}
