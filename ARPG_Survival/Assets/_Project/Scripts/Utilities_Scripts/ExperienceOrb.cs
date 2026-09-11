using UnityEngine;

public class ExperienceOrb : MonoBehaviour
{
    private float _xpAmount;

    public void SetXpValue(float value)
    {
        _xpAmount = value;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.TryGetComponent<PlayerLevelSystem>(out var levelSystem)) levelSystem.AddExperience(_xpAmount);
            Debug.Log($"Raccolti {_xpAmount} punti esperienza!");
            Destroy(gameObject);
        }
    }
}
