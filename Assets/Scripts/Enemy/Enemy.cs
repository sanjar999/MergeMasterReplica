using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Creature
{
    protected float _health = 100;

    public enum EnemyType { range, melee }

    protected Vector2Int _coord;
    protected EnemyType _enemyType;
    protected NavMeshAgent _agent;
    protected Transform _target;

    protected int _level = 1;
    protected float _damage = 2;

    private bool _isFight;

    protected Fight _fight;
    protected UnitSpawner _unitSpawner;

    public void SetUnitSpawner(UnitSpawner unitSpawner) { _unitSpawner = unitSpawner; }
    public void SetFight(Fight fight) { _fight = fight; }

    public int GetLevel() => _level;
    public void SetLevel(int level) { _level = level; }
    public void LevelUp() { _level++; OnLevelUp?.Invoke(); }

    public Vector2Int GetCoord() => _coord;
    public void SetCoord(Vector2Int coord) { _coord = coord; }

    public EnemyType GetUnitType() => _enemyType;
    public void SetUnitType(EnemyType enemyType) { _enemyType = enemyType; }

    private float damageOffset = 0;
    private List<Unit> _enemies;

    public float GetHealth() => _health;


    private void Start()
    {
        if (_fight)
            _fight.OnFight += () => _isFight = true;

        _agent = GetComponent<NavMeshAgent>();
        _enemies = _unitSpawner.GetUnits();
    }
    private void OnDisable()
    {
        if (_fight)
            _fight.OnFight -= () => _isFight = true;
    }

    private void Update()
    {
        if (_isFight)
        {
            Attack();
        }
    }

    protected virtual void Attack()
    {
        damageOffset += Time.deltaTime;

        if (!_unitSpawner.HasUnit())
        {
            _agent.isStopped = true;
            return;
        }

        if (!_target)
            _target = GetCloseEnemy(_enemies).gameObject.transform;
        else
        {
            _agent.SetDestination(_target.position);
            _agent.obstacleAvoidanceType = ObstacleAvoidanceType.HighQualityObstacleAvoidance;
            if (_agent.remainingDistance <= _agent.stoppingDistance && damageOffset > .3f)
            {
                damageOffset = 0;
                DealDamage(_target.GetComponent<Unit>(), _damage * _level);
            }
        }

        transform.LookAt(new Vector3(_target.position.x, transform.position.y, _target.position.z));

    }

    public Unit GetCloseEnemy(List<Unit> enemies)
    {
        float distance = float.MaxValue;
        Unit result = null;
        for (int i = 0; i < enemies.Count; i++)
        {
            if (enemies[i] != null && Vector3.Distance(transform.position, enemies[i].transform.position) < distance)
            {
                distance = Vector3.Distance(transform.position, enemies[i].transform.position);
                result = enemies[i];
            }
        }
        return result;
    }

    protected virtual void DealDamage(Unit target, float amount)
    {
        target.GetDamage(amount);
        OnDealDamage?.Invoke();

    }

    public void GetDamage(float amount)
    {
        _health -= amount;
        OnGetDamage?.Invoke();
        if (_health<=0)
        {
            Destroy(gameObject);
        }

    }

    public Action OnLevelUp;
    public Action OnGetDamage;
    public Action OnDealDamage;
}
