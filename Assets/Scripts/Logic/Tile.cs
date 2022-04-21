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

    private Material _mat;
    private bool _isGreen;
    private bool _isRed;

    private void Start()
    {
        _mat = GetComponent<MeshRenderer>().material;

    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    var cr = GetCreature();

    //    if (!_isGreen)
    //    {
    //        _mat.SetColor("_Color", _mat.GetColor("_Color") + new Color(0, 0, 0, .5f));
    //        _isGreen = true;
    //    }
    //}
    //private void OnTriggerExit(Collider other)
    //{
    //    if (_isGreen)
    //    {
    //        _mat.SetColor("_Color", _mat.GetColor("_Color") - new Color(0, 0, 0, .5f));
    //        _isGreen = false;

    //    }
    //}
}

