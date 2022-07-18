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
    private bool _isWon = false;
    public List<Enemy> GetEnemies() => _enemies;

    public bool HasEnemy
    {
        get
        {
            foreach (var enemy in _enemies)
                if (enemy != null)
                    return true;
            if (!_isWon)
            {
                Events.OnWin?.Invoke();
                _isWon = true;
            }
            return false;
        }
    }

    private void Start()
    {
        var currentStage = _stageManager.GetCurrentStage();
        _tiles = _tileSpawner.GetRedTiles();
        _level = Mathf.Clamp(currentStage / _levelUpStep, 1, int.MaxValue);
        _enemyAmount = Mathf.Clamp(currentStage / _amountIncraseStep, 1, 20);
        SpawnEnemeis();
    }

    private void SpawnEnemeis()
    {
        while (_enemyAmount != 0)
        {
            var randomRangedTile = GetRandomRangedTile(_tiles, _tileSpawner);

            if (!_tileSpawner.HasEmptyRedTile())
                return;

            if (randomRangedTile.HasUnit())
                continue;
            else
            {
                var enemyIndex = rnd.Range(0, _enemyTypes.Length);
                var instance = Instantiate(_enemyTypes[enemyIndex]);

                _enemies.Add(instance);
                randomRangedTile.SetCreature(instance);
                instance.SetUnitSpawner(_unitSpawner);
                instance.SetLevel(_level);
                //instance.SetTile(randomRangedTile);
                instance.transform.position = randomRangedTile.transform.position;
                instance.transform.SetParent(_parent);
            }
            _enemyAmount--;
        }
    }

    private Tile GetRandomMeleeTile(List<Tile> tiles, TileSpawner tileSpawner)
    {
        return tiles[rnd.Range(0, 2) + (tileSpawner.GetWidth()) * rnd.Range(0, tileSpawner.GetHeight())];
    }

    private Tile GetRandomRangedTile(List<Tile> tiles, TileSpawner tileSpawner)
    {
        return tiles[rnd.Range(0, 5) + (tileSpawner.GetWidth()) * rnd.Range(0, tileSpawner.GetHeight())];
    }
}