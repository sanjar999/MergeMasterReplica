using UnityEngine;

public class UnitMove : MonoBehaviour
{
    [SerializeField] private Transform _planePosition;
    [SerializeField] private MergeUnit _mergeUnit;

    private Vector3 _lastPosition;
    private Unit _currentUnit;
    private Camera _cam;
    private Plane _plane;

    private bool _isSelected = false;
    private bool _isUnselected = false;

    private void Start()
    {
        _plane = new Plane(Vector3.up, _planePosition.position);
        _cam = Camera.main;
    }
    private void Update()
    {
        Ray ray = _cam.ScreenPointToRay(Input.mousePosition);

        if (_currentUnit != null)
            MovingUnit(ray, _plane, _currentUnit);
        if (Input.GetMouseButtonDown(0) && !_isSelected)
            SelectUnit(ray, ref _currentUnit, ref _lastPosition);
        if (Input.GetMouseButtonUp(0) && !_isUnselected)
            UnselectUnit(ref _currentUnit, _mergeUnit);
    }

    private void MovingUnit(Ray ray, Plane plane, Unit currentUnit)
    {
        if (plane.Raycast(ray, out float distance))
        {
            var wP = ray.GetPoint(distance);
            var unitNewPosition = wP + Vector3.up;
            currentUnit.transform.position = unitNewPosition;
        }
    }

    private void SelectUnit(Ray ray, ref Unit currentUnit, ref Vector3 lastPosition)
    {
        Events.OnMoveStart?.Invoke();

        _isSelected = true;
        _isUnselected = false;

    

        if (Physics.Raycast(ray, out RaycastHit hit, 1000) && hit.collider.CompareTag("Unit"))
        {
            currentUnit = hit.collider.gameObject.GetComponentInParent<Unit>();
            if (currentUnit.IsFromScroller())
            {
                currentUnit.transform.parent = null;
                currentUnit.transform.eulerAngles =Vector3.zero;
            }
            lastPosition = currentUnit.transform.position;
            currentUnit.IsDragging = true;
        }
    }
    private void UnselectUnit(ref Unit currentUnit, MergeUnit mergeUnit)
    {
        _isSelected = false;
        _isUnselected = true;

        if (currentUnit != null && Physics.Raycast(currentUnit.GetRaycastPos(), Vector3.down, out RaycastHit hit, 100))
        {
            if (hit.collider.CompareTag("Tile"))
            {
                var tile = hit.collider.gameObject.GetComponent<Tile>();
                if (tile.HasUnit() && mergeUnit.TryMergeTwoUnit(currentUnit, (Unit)tile.GetCreature()))
                    ReplaceUnit(tile, currentUnit);
                else if (!tile.HasUnit())
                    ReplaceUnit(tile, currentUnit);
                else
                    currentUnit.transform.position = _lastPosition;
            }
            else
            {
                currentUnit.transform.position = _lastPosition;
            }

        }
        else if (currentUnit != null)
        {
            currentUnit.transform.position = _lastPosition;
            currentUnit.IsDragging = false;
        }

        currentUnit = null;
        Events.OnMoveEnd?.Invoke();
    }

    private void ReplaceUnit(Tile tile, Unit currentUnit)
    {
        currentUnit.transform.position = tile.transform.position;
        _lastPosition = tile.transform.position;
        tile.SetCreature(currentUnit);
        currentUnit.TryClearUnitTile();
        currentUnit.SetTile(tile);
    }

}