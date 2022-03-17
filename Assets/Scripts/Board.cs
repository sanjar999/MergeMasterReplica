using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    [SerializeField] Rect _movingAreaBounds;

    Unit[,] _unitsOnBoard;
    public Rect MovingArea { get => _movingAreaBounds; }
    private List<Unit> _units = new List<Unit>();

    public void AddUnit(Unit unit)
    {
        _units.Add(unit);
    }

    public void RemoveUnit(Unit unit)
    {
        _units.Remove(unit);
    }

    public List<Unit> GetUnits()
    {
        return _units;
    }

    void Start()
    {
        int x = (int)_movingAreaBounds.width;
        int y = (int)_movingAreaBounds.height;
        _unitsOnBoard = new Unit[x,y];
    }

    public void SetObjectToBoard(int x, int y, Unit unit)
    {
        _unitsOnBoard[x, y] = unit;
    }

    public Unit GetObjectFromBoard(int x, int y)
    {
        return _unitsOnBoard[x, y];
    }

}
