using UnityEngine;

public class UnitMove : MonoBehaviour
{
    [SerializeField] Transform _planePosition;
    [SerializeField] Board _board;
    [SerializeField] MergeUnit _mergeUnit;
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
        //Debug.DrawRay(_cam.transform.position, _cam.ScreenPointToRay(Input.mousePosition).direction * 100);
        Ray ray = _cam.ScreenPointToRay(Input.mousePosition);

        if (_plane.Raycast(ray, out float distance) && _currentUnit != null)
        {
            var wP = ray.GetPoint(distance);
            var yHalfScale = _currentUnit.transform.localScale.y * 0.5f;
            var unitNewPosition = wP + Vector3.up * yHalfScale;
            _currentUnit.transform.position = unitNewPosition;
        }

        if (Input.GetMouseButtonDown(0) && Physics.Raycast(ray, out RaycastHit hit, 1000) && hit.collider.CompareTag("Unit"))
        {
            _currentUnit = hit.collider.gameObject.GetComponent<Unit>();
            _lastPosition = _currentUnit.transform.position;
        }

        if (Input.GetMouseButtonUp(0) && _currentUnit != null)
        {
            var unitPos = _currentUnit.transform.position;
            var _roundedUnitPos = new Vector3(Mathf.RoundToInt(unitPos.x), unitPos.y, Mathf.RoundToInt(unitPos.z));
            if (!_board.MovingArea.Contains(new Vector2(_roundedUnitPos.x, _roundedUnitPos.z)))
            {
                _currentUnit.transform.position = _lastPosition;
            }
            else
            {
                var x = (int)(_roundedUnitPos.x - _board.MovingArea.x);
                var y = (int)(_roundedUnitPos.z - _board.MovingArea.y);

                if (_board.GetObjectFromBoard(x, y) != null && _currentUnit.tag == _board.GetObjectFromBoard(x, y).tag && _currentUnit != _board.GetObjectFromBoard(x, y))
                {
                    var mergeSucces = _mergeUnit.MergeTwoUnit(_currentUnit, _board.GetObjectFromBoard(x, y));
                    if (mergeSucces)
                    {
                        _board.SetObjectToBoard(_currentUnit.x, _currentUnit.y, null);
                        _board.SetObjectToBoard(x, y, _currentUnit);
                        _currentUnit.transform.position = _roundedUnitPos;
                    }
                    else
                    {
                        _currentUnit.transform.position = _lastPosition;
                    }

                }
                else if (_board.GetObjectFromBoard(x, y) == null)
                {
                    _board.SetObjectToBoard(_currentUnit.x, _currentUnit.y, null);
                    _board.SetObjectToBoard(x, y, _currentUnit);
                    _currentUnit.transform.position = _roundedUnitPos;
                }

                _currentUnit.x = x;
                _currentUnit.y = y;
            }
            _currentUnit = null;
        }
    }
}