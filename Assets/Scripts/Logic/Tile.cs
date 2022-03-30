using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private Vector2Int _coord;
    private Unit _unitOnTile;

    public bool HasUnit() => _unitOnTile;
    public Unit GetUnit() => _unitOnTile;
    public Vector2Int GetCoord() => _coord;
    public void SetCoord(int x, int y) { _coord.x = x; _coord.y = y; }
    public void SetUnit(Unit u) { _unitOnTile = u;}
}
