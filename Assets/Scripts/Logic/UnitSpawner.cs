using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitSpawner : MonoBehaviour
{
    [SerializeField] Fight _fight;
    [SerializeField] EnemySpawner _enemySpawner;
    [SerializeField] Unit[] _unitTypes;
    [SerializeField] Button _spawnButton;
    [SerializeField] TileSpawner _tileSpawner;
    [SerializeField] Transform _parent;

    private List<Unit> _units = new List<Unit>();
    private List<Tile> _tiles = new List<Tile>();
    public List<Unit> GetUnits() => _units;

    public bool HasUnit()
    {
        foreach (var enemy in _units)
        {
            if (enemy != null)
            {
                return true;
            }
        }
        return false;
    }

    private void Awake()
    {
        _spawnButton.onClick.AddListener(SpawnUnit);
    }
    private void Start()
    {
        _tiles = _tileSpawner.GetTiles();
        RestoreProgress();
    }

    private void SpawnUnit()
    {

        var randomTile = _tiles[UnityEngine.Random.Range(0, _tiles.Count)];

        if (_tileSpawner.HasEmptyTile() && randomTile.HasUnit())
            SpawnUnit();
        else if(!_tileSpawner.HasEmptyTile())
        {
            return;
        }
        else
        {
            int randomUnitIndex = UnityEngine.Random.Range(0, _unitTypes.Length);
            var instance = Instantiate(_unitTypes[randomUnitIndex]);
            _units.Add(instance);
            randomTile.SetUnit(instance);

            instance.SetUnitType((Unit.UnitType)randomUnitIndex);
            instance.SetFight(_fight);
            instance.SetEnemySpawner(_enemySpawner);
            instance.SetCoord(randomTile.GetCoord());
            instance.SetTile(randomTile);
            instance.transform.position = randomTile.transform.position;
        }

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

            var unitTile = _tileSpawner.GetTiles()[unitTileY + 4 * unitTileX];
            unitTile.SetUnit(instance);
            instance.SetTile(unitTile);
            //index to position
            instance.transform.position = unitTile.transform.position;
        }
    }

    public Action OnSpawn;
}
