using System.Collections.Generic;
using UnityEngine;

public class EnemyBoard : MonoBehaviour
{
    [SerializeField] Rect _movingAreaBounds;

    Enemy[,] _enemiesOnBoard;
    public Rect MovingArea { get => _movingAreaBounds; }
    private readonly List<Enemy> _boardEnemies = new List<Enemy>();

    public void AddEnemy(Enemy unit)
    {
        _boardEnemies.Add(unit);
    }

    public void RemoveEnemy(Enemy unit)
    {
        _boardEnemies.Remove(unit);
    }

    public List<Enemy> GetEnemies()
    {
        return _boardEnemies;
    }

    void Awake()
    {
        int x = (int)_movingAreaBounds.width;
        int y = (int)_movingAreaBounds.height;
        _enemiesOnBoard = new Enemy[x, y];
    }

    public void SetObjectToBoard(int x, int y, Enemy unit)
    {
        _enemiesOnBoard[x, y] = unit;
    }

    public Enemy GetObjectFromBoard(int x, int y)
    {
        return _enemiesOnBoard[x, y];
    }
}
