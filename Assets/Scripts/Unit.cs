using System;
using UnityEngine;
using UnityEngine.AI;

public class Unit : MonoBehaviour
{
    public int x;
    public int y;

    public Fight _fight;
    public EnemySpawner _enemySpawner;

    private int level = 1;
    private NavMeshAgent _agent;
    private GameObject _target;
    private bool _isFight;

    public int Level
    {
        get => level;
        set
        {
            level = value;
            GetComponent<Renderer>().material.SetColor("_Color", Color.Lerp(Color.white, Color.magenta * Color.yellow, (level - 1) / 4f));
            OnLevelUp?.Invoke();
        }
    }

    private void Start()
    {
        //subscribing on Start instead of OnEnable
        _agent = GetComponent<NavMeshAgent>();
        _fight.OnFight += Fight;
    }
    private void OnDisable()
    {
        _fight.OnFight -= Fight;
    }

    private void Update()
    {
        if (_isFight)
            _agent.SetDestination(_target.transform.position);
    }

    private void Fight(GameObject enemy)
    {
        if (enemy != null)
        {
            _agent.obstacleAvoidanceType = ObstacleAvoidanceType.HighQualityObstacleAvoidance;
            ChooseEnemy();
            _isFight = true;
        }
    }

    private void ChooseEnemy()
    {
        _target = _enemySpawner.GetRandomEnemy();
    }

    public Action OnLevelUp;
}
