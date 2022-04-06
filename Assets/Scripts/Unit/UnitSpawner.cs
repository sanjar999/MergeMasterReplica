using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitSpawner : MonoBehaviour
{
    [SerializeField] Fight _fight;
    [SerializeField] Unit[] _unitTypes;
    [SerializeField] EnemySpawner _enemySpawner;
    [SerializeField] TileSpawner _tileSpawner;
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
            OnLose?.Invoke();
            return false;
        }
    }

    private void Start()
    {
        _spawnMelee.onClick.AddListener(()=> SpawnUnit(Unit.UnitType.melee));
        _spawnRange.onClick.AddListener(()=> SpawnUnit(Unit.UnitType.range));
        _tiles = _tileSpawner.GetTiles();
        RestoreProgress();
    }

    private void SpawnUnit(Unit.UnitType unitType)
    {

        var randomTile = _tiles[UnityEngine.Random.Range(0, _tiles.Count)];

        if (!_tileSpawner.HasEmptyTile() || !randomTile.HasUnit())
        {
            if (!_tileSpawner.HasEmptyTile())
                return;
            else
            {
                var instance = Instantiate(_unitTypes[(int)unitType]);
                _units.Add(instance);
                randomTile.SetCreature(instance);

                instance.SetUnitType(unitType);
                instance.SetFight(_fight);
                instance.SetEnemySpawner(_enemySpawner);
                //instance.SetCoord(randomTile.GetCoord());
                instance.SetTile(randomTile);
                instance.transform.position = randomTile.transform.position;
            }
        }
        else SpawnUnit(unitType);

        OnSpawn?.Invoke();
    }

    private void RestoreProgress()
    {
        int unitCount = PlayerPrefs.GetInt("units_count", 0);
        for (int i = 0; i < unitCount; i++)
        {
            var unitLevel = PlayerPrefs.GetInt($"units_{i}_level");
            var unitTileX = PlayerPrefs.GetInt($"unitTile_{i}_x");
            var unitTileY = PlayerPrefs.GetInt($"unitTile_{i}_y");
            var unitType = PlayerPrefs.GetInt($"units_{i}_type");
            var instance = Instantiate(_unitTypes[unitType]);
            _units.Add(instance);

            //setting index off spawned unit
            instance.SetLevel(unitLevel);
            instance.SetUnitType((Unit.UnitType)unitType);
            instance.SetFight(_fight);
            instance.SetEnemySpawner(_enemySpawner);

            var unitTile = _tileSpawner.GetTiles()[unitTileY + _tileSpawner.GetHeight() * unitTileX];

            unitTile.SetCreature(instance);
            instance.SetTile(unitTile);
            //index to position
            instance.transform.position = unitTile.transform.position;
        }
    }

    public Action OnSpawn;
    public Action OnLose;
}
