using System;
using UnityEngine;

public class UnitMove : MonoBehaviour
{
    [SerializeField] Transform _planePosition;
    [SerializeField] MergeUnit _mergeUnit;
    Vector3 _lastPosition;
    Unit _currentUnit;
    Camera _cam;
    Plane _plane;

    bool _isSelected = false;
    bool _isUnselected = false;

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
            var yHalfScale = currentUnit.transform.localScale.y * 0.5f;
            var unitNewPosition = wP + Vector3.up;
            currentUnit.transform.position = unitNewPosition;
        }
    }

    private void SelectUnit(Ray ray, ref Unit currentUnit, ref Vector3 lastPosition)
    {
        _isSelected = true;
        _isUnselected = false;
        if (Physics.Raycast(ray, out RaycastHit hit, 1000) && hit.collider.CompareTag("Unit"))
        {
            print("select");

            currentUnit = hit.collider.gameObject.GetComponentInParent<Unit>();
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
            currentUnit.IsDragging = false;

            if (hit.collider.CompareTag("Tile"))
            {
                var tile = hit.collider.gameObject.GetComponent<Tile>();
                if (tile.HasUnit() && mergeUnit.MergeTwoUnit(currentUnit, (Unit)tile.GetCreature()))
                {
                    print('1');
                    currentUnit.transform.position = tile.transform.position;
                    _lastPosition = tile.transform.position;
                    tile.SetCreature(currentUnit);
                    currentUnit.ClearUnitTile();
                    currentUnit.SetTile(tile);
                }
                else if (tile.HasUnit())
                {
                    print('2');

                    currentUnit.transform.position = _lastPosition;
                }
                else
                {
                    print('3');
                    currentUnit.transform.position = tile.transform.position;
                    _lastPosition = tile.transform.position;
                    tile.SetCreature(currentUnit);
                    currentUnit.ClearUnitTile();
                    currentUnit.SetTile(tile);
                }

                OnMove?.Invoke();

            }
            else
            {
                print('4');

                currentUnit.transform.position = _lastPosition;
            }
            currentUnit = null;
        }
        else if (currentUnit != null)
        {
            print('5');

            currentUnit.IsDragging = false;

            currentUnit.transform.position = _lastPosition;
            currentUnit = null;
        }

    }
    public Action OnMove;
}
