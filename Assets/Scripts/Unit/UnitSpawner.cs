using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitSpawner : MonoBehaviour
{
    [SerializeField] EnemySpawner _enemySpawner;
    [SerializeField] TileSpawner _tileSpawner;
    [SerializeField] GameProgress _gameProgress;

    [SerializeField] Unit[] _unitTypes;
    [SerializeField] Button _spawnMelee;
    [SerializeField] Button _spawnRange;
    [SerializeField] Transform _parent;

    private List<Unit> _units = new List<Unit>();
    private List<Tile> _tiles = new List<Tile>();
    public List<Unit> GetUnits() => _units;

    public bool HasUnit
    {
        get
        {
            foreach (var enemy in _units)
                if (enemy != null)
                    return true;
            return false;
        }
    }

    private void Start()
    {
        _spawnMelee.onClick.AddListener(() => SpawnUnit(Unit.UnitType.melee));
        _spawnRange.onClick.AddListener(() => SpawnUnit(Unit.UnitType.range));
        _tiles = _tileSpawner.GetTiles();
        RestoreProgress();
    }

    private void SpawnUnit(Unit.UnitType unitType)
    {

        if (!_tileSpawner.HasEmptyTile())
            return;

        var randomTile = _tiles[Random.Range(0, _tiles.Count)];

        if (!randomTile.HasUnit())
        {
            var instance = Instantiate(_unitTypes[(int)unitType]);
            _units.Add(instance);
            randomTile.SetCreature(instance);
            instance.SetUnitType(unitType);
            instance.SetEnemySpawner(_enemySpawner);
            instance.SetTile(randomTile);
            instance.transform.position = randomTile.transform.position;
        }
        else SpawnUnit(unitType);

        Events.OnSpawn?.Invoke();
    }

    private void RestoreProgress()
    {
        int unitCount = _gameProgress.GetSvedUnitsCount();
        for (int i = 0; i < unitCount; i++)
        {
            var unitLevel = _gameProgress.GetSvedUnitLevel(i);
            var unitTileX = _gameProgress.GetSvedUnitIndexX(i);
            var unitTileY = _gameProgress.GetSvedUnitIndexY(i);
            var unitType = _gameProgress.GetSvedUnitType(i);
            var instance = Instantiate(_unitTypes[unitType]);
            _units.Add(instance);

            //setting index off spawned unit
            instance.SetLevel(unitLevel);
            instance.SetUnitType((Unit.UnitType)unitType);
            instance.SetEnemySpawner(_enemySpawner);

            var unitTile = _tileSpawner.GetTiles()[unitTileY + _tileSpawner.GetHeight() * unitTileX];

            unitTile.SetCreature(instance);
            instance.SetTile(unitTile);
            //index to position
            instance.transform.position = unitTile.transform.position;
        }
    }
}