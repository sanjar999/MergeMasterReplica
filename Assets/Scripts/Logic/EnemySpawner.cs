using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<Enemy> _enemies;

    public List<Enemy> GetEnemies() => _enemies;

    public bool HasEnemy()
    {
        foreach (var enemy in _enemies)
        {
            if (enemy != null)
            {
                return true;
            }
        }
        return false;
    }
    
}
