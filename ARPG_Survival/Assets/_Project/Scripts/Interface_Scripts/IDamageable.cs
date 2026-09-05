using UnityEngine;

public interface IDamageable
{
    /// <summary>
    /// Applica un ammontare di danno all'entità
    /// </summary>
    /// <param name="damage">Quantità di danno da infliggere</param>
    void TakeDamage(float damage);
}
