using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private Vector2 _coord;
    private Unit _unitOnTile;

    public bool HaveUnit() => _unitOnTile;
    public Unit GetUnit() => _unitOnTile;
    public void SetCoord(int x, int y) { _coord.x = x; _coord.y = y; }
    public void SetUnit(Unit u) { _unitOnTile = u;}
}
