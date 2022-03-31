using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] Enemy[] _enemyTypes;
    [SerializeField] Fight _fight;
    [SerializeField] UnitSpawner _unitSpawner;
    [SerializeField] EnemyBoard _enemyBoard;
    [SerializeField] Transform _parent;
    [SerializeField] int _enemyAmount;

    public List<Enemy> GetEnemies() => _enemyBoard.GetEnemies();

    private void Start()
    {
        SpawnEnemeis(_enemyAmount);
    }

    public bool HasEnemy()
    {
        foreach (var enemy in _enemyBoard.GetEnemies())
        {
            if (enemy != null)
            {
                return true;
            }
        }
        return false;
    }

    private void SpawnEnemeis(int amount)
    {
        while (amount != 0)
        {
            int x = Random.Range(0, (int)_enemyBoard.MovingArea.width);
            int y = Random.Range(0, (int)_enemyBoard.MovingArea.height);
            if (_enemyBoard.GetObjectFromBoard(x, y) == null)
            {
                var instance = Instantiate(_enemyTypes[0]);
                instance.SetCoord(new Vector2Int(x, y));
                instance.SetFight(_fight);
                instance.SetUnitSpawner(_unitSpawner);
                instance.transform.parent = _parent;
                instance.transform.position = new Vector3(x + (int)_enemyBoard.MovingArea.x, 1, y + (int)_enemyBoard.MovingArea.y);
                _enemyBoard.SetObjectToBoard(x, y, instance);
                _enemyBoard.AddEnemy(instance);
                amount--;
            }
        }
    }
}