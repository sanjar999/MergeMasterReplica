using UnityEngine;

public class Tile : MonoBehaviour
{
    private Vector2Int _coord;
    private Creature _creatureOnTile;

    public bool HasUnit() => _creatureOnTile;
    public Creature GetCreature() => _creatureOnTile;
    public Vector2Int GetCoord() => _coord;
    public void SetCoord(int x, int y) { _coord.x = x; _coord.y = y; }
    public void SetCreature(Creature u) { _creatureOnTile = u; }
}

