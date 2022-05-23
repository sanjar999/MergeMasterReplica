using System.Collections.Generic;
using UnityEngine;

public class TileSpawner : MonoBehaviour
{
    [SerializeField] private Transform _parent;
    [SerializeField] private Tile _greenTilePrefab;
    [SerializeField] private Tile _redTilePrefab;
    [SerializeField] private int _boardWidth;
    [SerializeField] private int _boardHeight;
    [SerializeField] private float _tileOffsetXStep;
    [SerializeField] private float _tileOffsetZStep;
    [SerializeField] private float _startXPos;
    [SerializeField] private float _startZPos;
    [SerializeField] private float _secondStartZPos;
    [SerializeField] private float _redStartZPos;
    [SerializeField] private float _secondRedStartZPos;

    private List<Tile> _greenTiles = new List<Tile>();
    private List<Tile> _redTiles = new List<Tile>();

    public List<Tile> GetTiles() => _greenTiles;
    public List<Tile> GetRedTiles() => _redTiles;
    public int GetHeight() => _boardHeight;
    public int GetWidth() => _boardWidth;

    private void Start()
    {
        SpawnTiles(_greenTilePrefab, _startZPos, _secondStartZPos, _greenTiles);
        SpawnTiles(_redTilePrefab, _redStartZPos, _secondRedStartZPos, _redTiles);
    }

    public bool HasEmptyTile()
    {
        foreach (var tile in _greenTiles)
            if (!tile.HasUnit())
                return true;
        return false;
    }

    public bool HasEmptyRedTile()
    {
        foreach (var tile in _redTiles)
            if (!tile.HasUnit())
                return true;
        return false;
    }

    private void SpawnTiles(Tile tile, float startZPos, float secondStartZpos, List<Tile> tileHolder)
    {
        float zPos;
        for (int x = 0; x < _boardWidth; x++)
        {
            if (x % 2 == 1)
                zPos = secondStartZpos;
            else 
                zPos = startZPos;

            for (int y = 0; y < _boardHeight; y++)
            {
                var instance = Instantiate(tile);
                instance.SetCoord(x, y);
                tileHolder.Add(instance);
                instance.transform.parent = _parent;
                instance.transform.position = new Vector3(_startXPos + _tileOffsetXStep * x,
                                                          instance.transform.position.y,
                                                          zPos + _tileOffsetZStep * y);
            }
        }
    }
}