using System;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private PlayerDataSO _playerData;
    public float CurrentHealth { get; private set; }
    public float CurrentStamina { get; private set; }
    public PlayerDataSO PlayerData => _playerData;

    public static event Action<float, float> OnHealthChanged;
    public static event Action<float, float> OnStaminaChanged;

    private void Awake()
    {
        if (_playerData != null)
        {
            CurrentHealth = _playerData.MaxHealth;
            CurrentStamina = _playerData.MaxStamina;
        }
    }
    private void Update()
    {
        RegenerateStamina();
    }
    public void TakeDamage(float damage)
    {
        if (CurrentHealth <= 0) return;

        float def = _playerData != null ? _playerData.Defense : 0;
        float finalDmg = Math.Max(damage - def, 1f);

        CurrentHealth -= finalDmg;
        CurrentHealth = Math.Max(CurrentHealth, 0f);

        OnHealthChanged?.Invoke(CurrentHealth, _playerData.MaxHealth);
        Debug.Log($"<color=magenta>[Player] Subiti {finalDmg} danni! Vita attuale: {CurrentHealth}/{_playerData.MaxHealth}</color>");

        if (CurrentHealth <= 0)
        {
            Debug.Log("<color=black>[Player] Il Player è morto!</color>");
        }
    }
    private void RegenerateStamina()
    {
        if (_playerData == null) return;
        if (CurrentStamina < _playerData.MaxStamina)
        {
            CurrentStamina += _playerData.StaminaRegenRate * Time.deltaTime;
            CurrentStamina = Mathf.Min(CurrentStamina, _playerData.MaxStamina);
            OnStaminaChanged?.Invoke(CurrentStamina, _playerData.MaxStamina);
        }
    }
    public bool UseStamina(float amount)
    {
        if (CurrentStamina >= amount)
        {
            CurrentStamina -= amount;
            Debug.Log($"<color=yellow>[Stamina Spesa] Usati {amount} di Stamina per l'Azione. Rimanente: {CurrentStamina:F1} / {_playerData.MaxStamina}</color>");
            OnStaminaChanged?.Invoke(CurrentStamina, _playerData.MaxStamina);
            return true;
        }
        Debug.LogWarning($"<color=red>[Stamina Insufficiente] Serve {amount}, ma hai solo {CurrentStamina:F1}!</color>");
        return false;
    }
}
