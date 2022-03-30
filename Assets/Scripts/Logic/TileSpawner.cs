using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSpawner : MonoBehaviour
{
    [SerializeField] Tile _tilePrefab;

    [SerializeField] int _boardWidth;
    [SerializeField] int _boardHeight;

    [SerializeField] float _tileXOffset;
    [SerializeField] float _tileZOffset;
    [SerializeField] float _tileOffsetXStep = 2f;
    [SerializeField] float _tileOffsetZStep = 1.5f;

    [SerializeField] float _startXPos = -4f;
    [SerializeField] float _startZPos = -7f;

    private List<Tile> _tiles = new List<Tile>();

    public List<Tile> GetTiles() => _tiles;
    public int GetHeight() => _boardHeight;

    private void Start()
    {
        SpawnTiles();
    }

    public bool HasEmptyTile()
    {
        foreach (var tile in _tiles)
        {
            if (!tile.HasUnit())
            {
                return true;
            }
        }
        return false;
    }

    private void SpawnTiles()
    {
        for (int x = 0; x < _boardWidth; x++)
        {
            _tileXOffset = _tileOffsetXStep * x;
            for (int y = 0; y < _boardHeight; y++)
            {
                if (y == 0)
                    _tileZOffset = 0;
                _tileZOffset += _tileOffsetZStep;

                var instance = Instantiate(_tilePrefab);
                instance.SetCoord(x,y);
                _tiles.Add(instance);
                //index to position
                instance.transform.position = new Vector3(_startXPos + _tileXOffset, instance.transform.position.y, _startZPos + _tileZOffset);
            }
        }
    }
}