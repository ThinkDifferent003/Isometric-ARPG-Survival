using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Projectile Settings")]
    [SerializeField] private float _speed;
    [SerializeField] private float _lifeTime;
    private float _dmg;
    private string _ownerTag;
    private Coroutine _deactivateCoroutine;

    public void Initialize(float baseDmg, string ownerTag = "Player")
    {
        _dmg = baseDmg;
        _ownerTag = ownerTag;
        if (_deactivateCoroutine != null) StopCoroutine(_deactivateCoroutine);
        _deactivateCoroutine = StartCoroutine(DeactivateAfterTime());
    }
    private void Update()
    {
        transform.Translate(Vector3.forward * _speed * Time.deltaTime);
    }
    private IEnumerator DeactivateAfterTime()
    {
        yield return new WaitForSeconds(_lifeTime);
        ReturnSelf();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(_ownerTag)) return;
        if (other.CompareTag("Projectile")) return;
        if (other.TryGetComponent<IDamageable>(out var target)) target.TakeDamage(_dmg);
        Debug.Log($"<color=orange>[Proiettile] Colpito: {other.name} per {_dmg} danni!</color>");
        ReturnSelf();
    }
    private void ReturnSelf()
    {
        if (_deactivateCoroutine != null) StopCoroutine(_deactivateCoroutine);
        ObjectPool.Instance.ReturnToPool(gameObject);
    }
}
