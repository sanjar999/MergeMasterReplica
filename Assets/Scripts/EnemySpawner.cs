using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> _enemies;

    public GameObject GetRandomEnemy()
    {
        return _enemies[Random.Range(0, _enemies.Count)];
    }

}
