using UnityEngine;
using UnityEngine.UI;

public class UnitSpawn : MonoBehaviour
{
    [SerializeField] Fight _fight;
    [SerializeField] EnemySpawner _enemySpawner;
    [SerializeField] Unit _unit;
    [SerializeField] Button _spawnButton;
    [SerializeField] Board _board;


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
                    var instance = Instantiate(_unit);
                    _board.SetObjectToBoard(x, y, instance);

                    //setting index off spawned unit
                    instance.x = x;
                    instance.y = y;
                    instance._fight = _fight;
                    instance._enemySpawner = _enemySpawner;
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
            var unitXIndex = PlayerPrefs.GetInt($"units_{i}_x");
            var unitYIndex = PlayerPrefs.GetInt($"units_{i}_y");
            var instance = Instantiate(_unit);
            _board.SetObjectToBoard(unitXIndex, unitYIndex, instance);

            //setting index off spawned unit
            instance.x = unitXIndex;
            instance.y = unitYIndex;
            instance.Level = unitLevel;
            instance._fight = _fight;
            instance._enemySpawner = _enemySpawner;
            _board.AddUnit(instance);

            var yHalfScale = instance.transform.localScale.y * 0.5f;
            //index to position
            instance.transform.position = new Vector3(unitXIndex + _board.MovingArea.x, yHalfScale, unitYIndex + _board.MovingArea.y);
        }
    }
}
