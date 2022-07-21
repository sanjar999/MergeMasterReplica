using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitSpawner : MonoBehaviour
{
    [SerializeField] EnemySpawner _enemySpawner;
    [SerializeField] TileSpawner _tileSpawner;
    [SerializeField] GameProgress _gameProgress;
    [SerializeField] GameObject _buttonMask;
    [SerializeField] Unit[] _unitTypes;
    [SerializeField] Button _spawnUnit;
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
        var randomUnit = (Unit.UnitType)Random.Range(0, (int)Unit.UnitType.length);
        _spawnUnit.onClick.AddListener(() => SpawnUnit());
        _tiles = _tileSpawner.GetTiles();
        RestoreProgress();
        if (_gameProgress.GetCoins() >= _gameProgress.GetUnitPrice())
            _buttonMask.SetActive(false);
        else
            _buttonMask.SetActive(true);


    }

    private void SpawnUnit()
    {
        if (!CanBuyUnit())
            return;

        var randomUnitType = (Unit.UnitType)Random.Range(0, (int)Unit.UnitType.length);

        if (!_tileSpawner.HasEmptyTile())
            return;

        var randomTile = _tiles[Random.Range(0, _tiles.Count)];

        if (!randomTile.HasUnit())
        {
            var instance = Instantiate(_unitTypes[(int)randomUnitType]);
            _units.Add(instance);
            randomTile.SetCreature(instance);
            instance.SetUnitType(randomUnitType);
            instance.SetEnemySpawner(_enemySpawner);
            instance.SetTile(randomTile);
            instance.transform.position = randomTile.transform.position;
            instance.transform.SetParent(_parent);
        }
        else SpawnUnit();

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
            instance.transform.SetParent(_parent);

        }
    }

    private bool CanBuyUnit()
    {
        float currUnitPrice = _gameProgress.GetUnitPrice();
        if (_gameProgress.GetCoins() >= currUnitPrice)
        {
            //_gameProgress.SetCoins(_gameProgress.GetCoins() - (int)currUnitPrice);
            Events.OnBuyUnit?.Invoke(-(int)currUnitPrice);
            var newPrice = currUnitPrice += currUnitPrice * .25f;
            _gameProgress.SetUnitPrice((int)newPrice);
            if (_gameProgress.GetCoins() < currUnitPrice)
            {
                _buttonMask.SetActive(true);
            }
            return true;
        }
        else
        {
            return false;
        }
    }
}