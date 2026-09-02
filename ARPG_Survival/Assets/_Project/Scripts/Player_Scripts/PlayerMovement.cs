using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Camera _mainCam;
    private NavMeshAgent _agent;

    [Header("Settings")]
    [SerializeField] private LayerMask _groundLayer;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
       
        if (_mainCam == null) _mainCam = Camera.main;
    }
    private void Update()
    {
        HandleInput();
    }
    private void HandleInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = _mainCam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, _groundLayer)) SetDestination(hit.point);
        }
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
