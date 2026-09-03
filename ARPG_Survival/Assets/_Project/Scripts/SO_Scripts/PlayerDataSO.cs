using UnityEngine;

[CreateAssetMenu(fileName = "NewPlayerData", menuName = "ARPG/PlayerData", order = 0)]
public class PlayerDataSO : ScriptableObject
{
    [Header("Base Stats")]
    [Tooltip("Punti vita massimi iniziali")]
    [SerializeField] private float _maxHealth;

    [Tooltip("Punti Difesa per ridurre il danno")]
    [SerializeField] private float _defense;

    [Tooltip("Stamina massima per lo sparo e/scatto")]
    [SerializeField] private float _maxStamina;

    [Tooltip("Valore di Attacco base per il calcolo dei danni")]
    [SerializeField] private float _attack;

    [Tooltip("Percentuale di Fortuna per colpi critici")]
    [SerializeField] private float _luck;

    [Header("Stamina & Dash Settings")]
    [SerializeField] private float _dashStaminaCost;
    [SerializeField] private float _staminaRegenRate;

    public float MaxHealth => _maxHealth;
    public float Defense => _defense;
    public float MaxStamina => _maxStamina;
    public float Attack => _attack;
    public float Luck => _luck;
    public float DashStaminaCost => _dashStaminaCost;
    public float StaminaRegenRate => _staminaRegenRate;
}
