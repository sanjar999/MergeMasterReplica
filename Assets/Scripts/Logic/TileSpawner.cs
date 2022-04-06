using System.Collections.Generic;
using UnityEngine;

public class TileSpawner : MonoBehaviour
{
    [SerializeField] Tile _greenTilePrefab;
    [SerializeField] Tile _redTilePrefab;

    [SerializeField] int _boardWidth;
    [SerializeField] int _boardHeight;

    [SerializeField] float _tileXOffset;
    [SerializeField] float _tileZOffset;
    [SerializeField] float _tileOffsetXStep = 2f;
    [SerializeField] float _tileOffsetZStep = 1.5f;

    [SerializeField] float _startXPos = -4f;
    [SerializeField] float _startZPos = -7f;
    [SerializeField] float _redStartZPos = -0.75f;
    [SerializeField] Transform _parent;

    private List<Tile> _greenTiles = new List<Tile>();
    private List<Tile> _redTiles = new List<Tile>();

    public List<Tile> GetTiles() => _greenTiles;
    public List<Tile> GetRedTiles() => _redTiles;
    public int GetHeight() => _boardHeight;
    public int GetWidth() => _boardWidth;

    private void Start()
    {
        SpawnTiles(_greenTilePrefab, _startZPos, _greenTiles);
        SpawnTiles(_redTilePrefab, _redStartZPos, _redTiles);
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

    private void SpawnTiles(Tile tile, float startZPos, List<Tile> tileHolder)
    {
        for (int x = 0; x < _boardWidth; x++)
        {
            _tileXOffset = _tileOffsetXStep * x;
            for (int y = 0; y < _boardHeight; y++)
            {
                if (y == 0)
                    _tileZOffset = 0;
                _tileZOffset += _tileOffsetZStep;

                var instance = Instantiate(tile);
                instance.SetCoord(x, y);
                tileHolder.Add(instance);
                //index to position
                instance.transform.position = new Vector3(_startXPos + _tileXOffset,
                                                           instance.transform.position.y,
                                                          startZPos + _tileZOffset);

                instance.transform.parent = _parent;
            }
        }
    }
}