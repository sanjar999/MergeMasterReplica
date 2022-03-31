using System;
using UnityEngine.AI;

public class Enemy : Creature
{

    public enum EnemyType { range, melee }

    protected EnemyType _enemyType;
    protected UnitSpawner _unitSpawner;

    public void SetUnitSpawner(UnitSpawner unitSpawner) { _unitSpawner = unitSpawner; }

    public EnemyType GetUnitType() => _enemyType;
    public void SetUnitType(EnemyType enemyType) { _enemyType = enemyType; }

    private void Start()
    {
        _fight.OnFight += () => _isFight = true;
        _unitSpawner.OnSpawn += GetEnemies;

        _agent = GetComponent<NavMeshAgent>();
        GetEnemies();
    }

    private void OnDisable()
    {
        _fight.OnFight -= () => _isFight = true;
        _unitSpawner.OnSpawn -= GetEnemies;

    }

    private void Update()
    {
        if (_isFight)
            Attack();
    }

    private Unit CreatureToUnit(Creature c) => c as Unit;
    private void GetEnemies() { _enemies = _unitSpawner.GetUnits().ConvertAll(new Converter<Unit, Creature>(CreatureToUnit)); }
    protected override void Attack()
    {
        if (!_unitSpawner.HasUnit)
        {
            _agent.isStopped = true;
            return;
        }
        base.Attack();
    }
}
