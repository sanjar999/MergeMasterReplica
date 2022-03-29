using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitSpawner : MonoBehaviour
{
    [SerializeField] Fight _fight;
    [SerializeField] EnemySpawner _enemySpawner;
    [SerializeField] Unit[] _unitTypes;
    [SerializeField] Button _spawnButton;
    [SerializeField] Board _board;
    [SerializeField] Transform _parent;

    public List<Unit> GetUnits() => _board.GetUnits();

    public bool HasUnit()
    {
        foreach (var enemy in _board.GetUnits())
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
        RestoreProgress();
    }

    private void SpawnUnit()
    {
        for (int x = 0; x < (int)_board.MovingArea.width; x++)
        {
            for (int y = 0; y < (int)_board.MovingArea.height; y++)
            {
                var unit = _board.GetObjectFromBoard(x, y);
                if (unit == null)
                {

                    int randomUnitIndex = Random.Range(0, _unitTypes.Length);
                    var instance = Instantiate(_unitTypes[randomUnitIndex]);

                    _board.SetObjectToBoard(x, y, instance);

                    //setting index off spawned unit
                    instance.SetCoord(new Vector2Int(x,y));
                    instance.SetUnitType((Unit.UnitType)randomUnitIndex);
                    instance.SetFight(_fight);
                    instance.SetEnemySpawner(_enemySpawner);
                    _board.AddUnit(instance);

                    var yHalfScale = instance.transform.localScale.y * 0.5f;
                    //index to position
                    instance.transform.position = new Vector3(x + _board.MovingArea.x, yHalfScale, y + _board.MovingArea.y);
                    return;
                }
            }
        }
    }

    private void RestoreProgress()
    {
        int unitCount = PlayerPrefs.GetInt("units_count", 0);
        for (int i = 0; i < unitCount; i++)
        {

            var unitLevel = PlayerPrefs.GetInt($"units_{i}_level");
            var unitXCoord = PlayerPrefs.GetInt($"units_{i}_x");
            var unitYCoord = PlayerPrefs.GetInt($"units_{i}_y");
            var unitType = PlayerPrefs.GetInt($"units_{i}_type");
            var instance = Instantiate(_unitTypes[unitType]);

            _board.SetObjectToBoard(unitXCoord, unitYCoord, instance);

            //setting index off spawned unit
            instance.SetCoord(new Vector2Int(unitXCoord,unitYCoord));
            instance.SetLevel(unitLevel);
            instance.SetUnitType((Unit.UnitType)unitType);
            instance.SetFight(_fight);
            instance.SetEnemySpawner(_enemySpawner);

            _board.AddUnit(instance);

            var yHalfScale = instance.transform.localScale.y * 0.5f;
            //index to position
            instance.transform.position = new Vector3(unitXCoord + _board.MovingArea.x, yHalfScale, unitYCoord + _board.MovingArea.y);
        }
    }
}
