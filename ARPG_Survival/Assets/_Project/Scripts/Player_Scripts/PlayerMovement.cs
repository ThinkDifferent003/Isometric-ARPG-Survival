using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(PlayerStats))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Camera _mainCam;
    private NavMeshAgent _agent;
    private PlayerStats _playerStats;

    [Header("Settings")]
    [SerializeField] private LayerMask _groundLayer;

    [Header("DashSettings")]
    [SerializeField] private float _dashSpeedMultiplier;
    [SerializeField] private float _dashDuration;
    [SerializeField] private float _doubleClickThreshold;
    private float _lastClickTime;
    private bool _isDashing;
    private float _originalSpeed;
    private float _originalAcceleration;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _playerStats = GetComponent<PlayerStats>();
        _originalSpeed = _agent.speed;
        _originalAcceleration = _agent.acceleration;
        if (_mainCam == null) _mainCam = Camera.main;
    }
    private void Update()
    {
        if (_isDashing) return;
        HandleInput();
    }
    private void HandleInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = _mainCam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, _groundLayer))
            {
                bool isDoubleClick = (Time.time - _lastClickTime) <= _doubleClickThreshold;
                _lastClickTime = Time.time;
                if (isDoubleClick)
                {
                    if (_playerStats.UseStamina(_playerStats.PlayerData.DashStaminaCost))
                    {
                        StartCoroutine(PerformDash(hit.point));
                        return;
                    }
                }
                SetDestination(hit.point);
            }
        }
    }
    private IEnumerator PerformDash(Vector3 targetPos)
    {
        _isDashing = true;
        _agent.speed = _originalSpeed * _dashSpeedMultiplier;
        _agent.acceleration = 100f;
        _agent.SetDestination(targetPos);

        yield return new WaitForSeconds(_dashDuration);

        _agent.speed = _originalSpeed;
        _agent.acceleration = _originalAcceleration;
        _isDashing = false;
    }
    public void SetDestination(Vector3 targetPos)
    {
        if (_agent != null && _agent.enabled) _agent.SetDestination(targetPos); 
    }
    public void StopMovement()
    {
        if (_agent != null && _agent.enabled) _agent.ResetPath();
    }
}
