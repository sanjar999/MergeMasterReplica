using UnityEngine;

public class UnitMove : MonoBehaviour
{
    [SerializeField] Transform _planePosition;
    [SerializeField] Board _board;
    [SerializeField] MergeUnit _mergeUnit;
    [SerializeField] Vector3 _gridSize;
    Vector3 _lastPosition;
    Unit _currentUnit;
    Camera _cam;
    Plane _plane;

    private void Start()
    {
        _plane = new Plane(Vector3.up, _planePosition.position);
        _cam = Camera.main;
    }
    private void Update()
    {
        Ray ray = _cam.ScreenPointToRay(Input.mousePosition);

        MovingUnit(ray, _plane, _currentUnit);
        SelectUnit(ray,ref _currentUnit,ref _lastPosition);
        UnselectUnit(ref _currentUnit, _board, _mergeUnit);
    }

    private void MovingUnit(Ray ray, Plane plane, Unit currentUnit)
    {
        if (plane.Raycast(ray, out float distance) && currentUnit != null)
        {
            var wP = ray.GetPoint(distance);
            var yHalfScale = currentUnit.transform.localScale.y * 0.5f;
            var unitNewPosition = wP + Vector3.up * yHalfScale;
            currentUnit.transform.position = unitNewPosition;
        }
    }

    private void SelectUnit(Ray ray,ref Unit currentUnit,ref Vector3 lastPosition)
    {
        if (Input.GetMouseButtonDown(0) && Physics.Raycast(ray, out RaycastHit hit, 1000) && hit.collider.CompareTag("Unit"))
        {
            currentUnit = hit.collider.gameObject.GetComponentInParent<Unit>();
            lastPosition = currentUnit.transform.position;
        }
    }
    private void UnselectUnit(ref Unit currentUnit, Board board, MergeUnit mergeUnit)
    {
        if (Input.GetMouseButtonUp(0) && currentUnit != null)
        {
            var unitPos = currentUnit.transform.position;
            var _roundedUnitPos = new Vector3(
                Mathf.Round((unitPos.x / _gridSize.x) * _gridSize.x),
                unitPos.y,
                Mathf.Round((unitPos.z / _gridSize.z) * _gridSize.z));
            var isOnBoard = board.MovingArea.Contains(new Vector2(_roundedUnitPos.x, _roundedUnitPos.z));
            var x = (int)(_roundedUnitPos.x - board.MovingArea.x);
            var y = (int)(_roundedUnitPos.z - board.MovingArea.y);
            Unit unitOnNewPosition = null;

            if (isOnBoard)
            {
                unitOnNewPosition = board.GetObjectFromBoard(x, y);
            }
            else
            {
                currentUnit.transform.position = _lastPosition;
                currentUnit = null;
                return;
            }


            if (unitOnNewPosition == null || mergeUnit.MergeTwoUnit(currentUnit, unitOnNewPosition))
            {
                var coord = currentUnit.GetCoord();
                board.SetObjectToBoard(coord.x, coord.y, null);
                board.SetObjectToBoard(x, y, currentUnit);
                currentUnit.transform.position = _roundedUnitPos;
                currentUnit.SetCoord(new Vector2Int(x,y));

            }
            else
            {
                currentUnit.transform.position = _lastPosition;
            }
            currentUnit = null;
        }
    }
}
