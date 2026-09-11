using UnityEngine;
using UnityEngine.UI;

public class MouseCursorWorld : MonoBehaviour
{
    [SerializeField] private Transform _cursorIndicator;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private LayerMask _enemyLayer;

    private GameObject _currentSelectionRing;

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit groundHit, 100f, _groundLayer))
        {
            if (_cursorIndicator != null) _cursorIndicator.position = groundHit.point + Vector3.up * 0.02f;
        }

        if (Physics.Raycast(ray, out RaycastHit enemyHit, 100f, _enemyLayer))
        {
            if (enemyHit.collider.CompareTag("Enemy"))
            {
                Transform ringTransform = enemyHit.collider.transform.Find("SelectionRing");
                if (ringTransform == null && enemyHit.collider.transform.parent != null) ringTransform = enemyHit.collider.transform.parent.Find("SelectionRing");
                if (ringTransform != null)
                {
                    GameObject targetRing = ringTransform.gameObject;
                    if (_currentSelectionRing != targetRing)
                    {
                        ClearSelection();
                        _currentSelectionRing = targetRing;
                        _currentSelectionRing.SetActive(true);
                    }
                    return;
                }
            }
        }
        ClearSelection();
    }
    private void ClearSelection()
    {
        if (_currentSelectionRing != null)
        {
            _currentSelectionRing.SetActive(false);
            _currentSelectionRing = null;
        }
    }
}
