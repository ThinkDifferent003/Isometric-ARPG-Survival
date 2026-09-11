using UnityEngine;

public class PlayerLevelSystem : MonoBehaviour
{
    [Header("Data Source")]
    [SerializeField] private PlayerDataSO _baseData;

    [Header("Level & XP")]
    public int CurrentLevel;
    public float CurrentXp;
    public float XpToNextLevel;

    [Header("XP Progression Curve")]
    [SerializeField] private float _baseXpRequirement;
    [SerializeField] private float _xpGrowthExponent;

    [Header("Stats Settings")]
    [SerializeField] private int _minStatPoints;
    [SerializeField] private int _maxStatPoints;

    [Header("Current Runtime Stats")]
    public float MaxHealth { get; private set; }
    public float CurrentHealth { get; private set; }
    public float Defense { get; private set; }
    public float MaxStamina { get; private set; }
    public float Attack { get; private set; }
    public float Luck { get; private set; }

    private void Awake()
    {
        InitializeStatsFromSO();
        XpToNextLevel = CalculateXpRequirement(CurrentLevel);
    }
    private void Update()
    {
        // Premendo "L" sulla tastiera forza un Level Up immediato per il test
        if (Input.GetKeyDown(KeyCode.L))
        {
            Debug.Log("<color=magenta>[TEST] Forzato Level Up con il tasto L!</color>");
            LevelUp();
        }
    }
    private void InitializeStatsFromSO()
    {
        if (_baseData == null) return;
        MaxHealth = _baseData.MaxHealth;
        CurrentHealth = _baseData.MaxHealth;
        Defense = _baseData.Defense;
        MaxStamina = _baseData.MaxStamina;
        Attack = _baseData.Attack;
        Luck = _baseData.Luck;
    }
    private float CalculateXpRequirement(int level) => Mathf.Round(_baseXpRequirement * Mathf.Pow(level, _xpGrowthExponent));
    public void AddExperience(float amount)
    {
        CurrentXp += amount;
        Debug.Log($"<color=cyan>[XP System] Guadagnati {amount} XP. Totale: {CurrentXp}/{XpToNextLevel}</color>");
        while (CurrentLevel >= XpToNextLevel)
        {
            CurrentXp -= XpToNextLevel;
            LevelUp();
        }
    }
    private void LevelUp()
    {
        CurrentLevel++;
        XpToNextLevel = CalculateXpRequirement(CurrentLevel);
        Debug.Log($"<color=yellow>★ LEVEL UP! Sei diventato Livello {CurrentLevel}! ★</color>");
        DistributeRandomStats();
    }
    private void DistributeRandomStats()
    {
        int totalPointsToDistribute = Random.Range(_minStatPoints, _maxStatPoints + 1);
        float hpBonus = 0;
        float atkBonus = 0;
        float defBonus = 0;
        float staminaBonus = 0;
        float luckBonus = 0;

        for ( int i = 0; i < totalPointsToDistribute; i++ )
        {
            int randomStatIndex = Random.Range(0, 5);
            switch (randomStatIndex)
            {
                case 0:
                    hpBonus += 10f;
                    break;
                case 1:
                    atkBonus += 1f;
                    break;
                case 2:
                    defBonus += 1f;
                    break;
                case 3:
                    staminaBonus += 5f;
                    break;
                case 4:
                    luckBonus += 0.5f;
                    break;
            }
        }
        MaxHealth += hpBonus;
        CurrentHealth += hpBonus;
        Attack += atkBonus;
        Defense += defBonus;
        MaxStamina += staminaBonus;
        Luck += luckBonus;

        Debug.Log($"<color=green>[STATS INCREASE] Punti usati: {totalPointsToDistribute} | " +
                  $"+{hpBonus} HP | +{atkBonus} ATK | +{defBonus} DEF | +{staminaBonus} STAMINA | +{luckBonus}% LUCK</color>");
    }
}
