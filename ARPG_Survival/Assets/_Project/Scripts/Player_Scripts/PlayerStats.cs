using System;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private PlayerDataSO _playerData;

    public float CurrentStamina {  get; private set; }
    public PlayerDataSO PlayerData => _playerData;
    public static event Action<float, float> OnStaminaChanged;

    private void Awake()
    {
        if (_playerData != null) CurrentStamina = _playerData.MaxStamina;
    }
    private void Update()
    {
        RegenerateStamina();
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
