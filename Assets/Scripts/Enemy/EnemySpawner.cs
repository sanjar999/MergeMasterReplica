using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] Fight _fight;
    [SerializeField] Enemy[] _enemyTypes;
    [SerializeField] UnitSpawner _unitSpawner;
    [SerializeField] TileSpawner _tileSpawner;
    [SerializeField] Transform _parent;
    [SerializeField] int _enemyAmount;

    private List<Enemy> _enemies = new List<Enemy>();
    private List<Tile> _tiles = new List<Tile>();
    public List<Enemy> GetEnemies() => _enemies;


    public bool HasEnemy
    {
        get
        {
            foreach (var enemy in _enemies)
                if (enemy != null)
                    return true;
            return false;
        }
    }

    private void Start()
    {
        _tiles = _tileSpawner.GetRedTiles();
        SpawnEnemeis(_enemyAmount);
    }

    private void SpawnEnemeis(int amount)
    {
        while (amount != 0)
        {
            var randomRangedTile = GetRandomRangedTile(_tiles, _tileSpawner);

            if (_tileSpawner.HasEmptyRedTile() && randomRangedTile.HasUnit())
                continue;
            else if (!_tileSpawner.HasEmptyRedTile())
                return;
            else
            {
                var instance = Instantiate(_enemyTypes[0]);

                _enemies.Add(instance);
                randomRangedTile.SetCreature(instance);
                instance.SetFight(_fight);
                instance.SetUnitSpawner(_unitSpawner);
                //instance.SetTile(randomRangedTile);
                instance.transform.position = randomRangedTile.transform.position;
            }
            amount--;
        }
    }

    private Tile GetRandomMeleeTile(List<Tile> tiles, TileSpawner tileSpawner)
    {
        return tiles[UnityEngine.Random.Range(0, 2) + (tileSpawner.GetWidth() - 1) * UnityEngine.Random.Range(0, tileSpawner.GetHeight())];
    }

    private Tile GetRandomRangedTile(List<Tile> tiles, TileSpawner tileSpawner)
    {
        return tiles[UnityEngine.Random.Range(2, 4) + (tileSpawner.GetWidth() - 1) * UnityEngine.Random.Range(0, tileSpawner.GetHeight())];
    }
    public Action OnSpawn;

}