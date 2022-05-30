using System.Collections.Generic;
using UnityEngine;
using rnd = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] Enemy[] _enemyTypes;
    [SerializeField] UnitSpawner _unitSpawner;
    [SerializeField] TileSpawner _tileSpawner;
    [SerializeField] StageManager _stageManager;
    [SerializeField] Transform _parent;
    [SerializeField] int _enemyAmount;
    [SerializeField] int _levelUpStep = 2;
    [SerializeField] int _amountIncraseStep = 3;

    private List<Enemy> _enemies = new();
    private List<Tile> _tiles = new();
    private int _level = 1;

    public List<Enemy> GetEnemies() => _enemies;

    public bool HasEnemy()
    {
        foreach (var enemy in _enemies)
            if (enemy != null)
                return true;
        return false;
    }

    private void Start()
    {
        var currentStage = _stageManager.GetCurrentStage();
        _tiles = _tileSpawner.GetRedTiles();
        _level = Mathf.Clamp(currentStage / _levelUpStep, 1, int.MaxValue);
        _enemyAmount = Mathf.Clamp(currentStage / _amountIncraseStep, 1 , int.MaxValue);
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
                var enemyIndex = UnityEngine.Random.Range(0, _enemyTypes.Length);
                var instance = Instantiate(_enemyTypes[enemyIndex]);

                _enemies.Add(instance);
                randomRangedTile.SetCreature(instance);
                instance.SetUnitSpawner(_unitSpawner);
                instance.SetLevel(_level);
                //instance.SetTile(randomRangedTile);
                instance.transform.position = randomRangedTile.transform.position;
            }
            amount--;
        }
    }

    private Tile GetRandomMeleeTile(List<Tile> tiles, TileSpawner tileSpawner)
    {
        return tiles[rnd.Range(0, 2) + (tileSpawner.GetWidth() - 1) * rnd.Range(0, tileSpawner.GetHeight())];
    }

    private Tile GetRandomRangedTile(List<Tile> tiles, TileSpawner tileSpawner)
    {
        return tiles[rnd.Range(0, 4) + (tileSpawner.GetWidth() - 1) * rnd.Range(0, tileSpawner.GetHeight())];
    }

}