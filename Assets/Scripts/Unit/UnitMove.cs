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

    private void Start()
    {
        _plane = new Plane(Vector3.up, _planePosition.position);
        _cam = Camera.main;
    }
    private void Update()
    {
        Ray ray = _cam.ScreenPointToRay(Input.mousePosition);

        MovingUnit(ray, _plane, _currentUnit);
        SelectUnit(ray, ref _currentUnit, ref _lastPosition);
        UnselectUnit(ref _currentUnit, _mergeUnit);
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

    private void SelectUnit(Ray ray, ref Unit currentUnit, ref Vector3 lastPosition)
    {
        if (Input.GetMouseButtonDown(0) && Physics.Raycast(ray, out RaycastHit hit, 1000) && hit.collider.CompareTag("Unit"))
        {
            currentUnit = hit.collider.gameObject.GetComponentInParent<Unit>();
            lastPosition = currentUnit.transform.position;
        }
    }
    private void UnselectUnit(ref Unit currentUnit, MergeUnit mergeUnit)
    {
        if (Input.GetMouseButtonUp(0) && currentUnit != null && Physics.Raycast(currentUnit.GetRaycastPos(), Vector3.down, out RaycastHit hit, 100))
        {
            if (hit.collider.CompareTag("Tile"))
            {

                var tile = hit.collider.gameObject.GetComponent<Tile>();
                if (tile.HasUnit() && mergeUnit.MergeTwoUnit(currentUnit, (Unit)tile.GetCreature()))
                {
                    currentUnit.transform.position = tile.transform.position;
                    tile.SetCreature(currentUnit);
                    currentUnit.ClearUnitTile();
                    currentUnit.SetTile(tile);
                }
                else if (tile.HasUnit())
                    currentUnit.transform.position = _lastPosition;
                else
                {
                    currentUnit.transform.position = tile.transform.position;
                    tile.SetCreature(currentUnit);
                    currentUnit.ClearUnitTile();
                    currentUnit.SetTile(tile);
                }

                OnMove?.Invoke();

            }
            else
                currentUnit.transform.position = _lastPosition;
            currentUnit = null;
        }
    }

    public Action OnMove;
}