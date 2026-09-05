using UnityEngine;

[RequireComponent(typeof(PlayerStats))]
public class PlayerCombat : MonoBehaviour
{
    [Header("Ranged Attack Settings")]
    [SerializeField] private GameObject _projectilePrefab;
    [SerializeField] private Transform _firePoint;
    [SerializeField] private float _fireRate;
    [SerializeField] private LayerMask _aimLayer;

    private Camera _mainCam;
    private PlayerStats _playerStats;
    private float _nextFireTime;

    private void Awake()
    {
        _mainCam = Camera.main;
        _playerStats = GetComponent<PlayerStats>();
    }
    private void Update()
    {
        HandleRangedInput();
    }
    private void HandleRangedInput()
    {
        if (Input.GetMouseButtonDown(1) && Time.time >= _nextFireTime)
        {
            if (TryShoot()) _nextFireTime = Time.time + _fireRate;
        }
    }
    private bool TryShoot()
    {
        Ray ray = _mainCam.ScreenPointToRay(Input.mousePosition);
        if (!Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, _aimLayer)) return false;
        
        if (_playerStats == null || _playerStats.PlayerData == null) return false;

        float cost = _playerStats.PlayerData.RangedStaminaCost;
        if (!_playerStats.UseStamina(cost))
        {
            Debug.LogWarning("<color=red>[Combat] Stamina insufficiente per sparare!</color>");
            return false;
        }

        Vector3 targetPoint = hit.point;
        Vector3 dir = targetPoint - _firePoint.position;
        dir.y = 0;

        if (dir != Vector3.zero) _firePoint.rotation = Quaternion.LookRotation(dir);
        GameObject projObj = ObjectPool.Instance.Get();
        projObj.transform.position = _firePoint.position;
        projObj.transform.rotation = _firePoint.rotation;

        Projectile proj = projObj.GetComponent<Projectile>();
        float atk = _playerStats.PlayerData.Attack;
        if (proj != null) proj.Initialize(atk);

        Debug.Log($"<color=cyan>[Combat] Sparato proiettile con danno base: {atk}</color>");
        return true;
    }
}
