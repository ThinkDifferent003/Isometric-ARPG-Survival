using UnityEngine;

public class MeleeHitbox : MonoBehaviour
{
    private float _dmg;

    public void Inizialize(float dmgAtk)
    {
        _dmg = dmgAtk;
        Debug.Log($"<color=yellow>[Hitbox] Inizializzata con danno: {_dmg}</color>");
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"<color=white>[Hitbox] Contatto rilevato con: {other.name} (Tag: {other.tag})</color>");
        if (other.CompareTag("Player") && other.TryGetComponent<IDamageable>(out var damageable))
        {
            damageable.TakeDamage(_dmg);
            Debug.Log($"<color=red>[Melee Attack] Il nemico ha colpito {other.name} per {_dmg} danni!</color>");
        }
    }
}
