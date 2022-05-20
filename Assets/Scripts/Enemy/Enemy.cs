using System;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Creature
{
    [SerializeField] protected float _lvlUpHpIncrease = 10f;
    [SerializeField] protected float _lvlUpDmgIncrease = .4f;

    public enum EnemyType { range, melee }

    protected EnemyType _enemyType;
    protected UnitSpawner _unitSpawner;

    public void SetUnitSpawner(UnitSpawner unitSpawner) { _unitSpawner = unitSpawner; }

    public EnemyType GetUnitType() => _enemyType;
    public void SetUnitType(EnemyType enemyType) { _enemyType = enemyType; }

    private void Start()
    {
        _fight.OnFight += () => _isFight = true;
        _fight.OnFight += StartFightAnim;
        _unitSpawner.OnSpawn += GetEnemies;

        _agent = GetComponent<NavMeshAgent>();
        GetEnemies();
    }

    private void OnDisable()
    {
        _fight.OnFight -= () => _isFight = true;
        _fight.OnFight -= StartFightAnim;
        _unitSpawner.OnSpawn -= GetEnemies;

    }

    private void Update()
    {
        if (_isFight)
            Attack();
        _animator.SetFloat("Blend", _agent.velocity.magnitude);
    }

    public override void SetLevel(int level)
    {
        base.SetLevel(level);
        _health = _level * _lvlUpHpIncrease;
        _damage = _level * _lvlUpDmgIncrease;
    }

    public override void LevelUp()
    {
        base.LevelUp();
        _health = _level * _lvlUpHpIncrease;
        _damage = _level * _lvlUpDmgIncrease;
    }

    private Unit CreatureToUnit(Creature c) => c as Unit;
    private void GetEnemies() { _enemies = _unitSpawner.GetUnits().ConvertAll(new Converter<Unit, Creature>(CreatureToUnit)); }
    protected override void Attack()
    {
        if (!_unitSpawner.HasUnit)
        {
            _agent.isStopped = true;
            _animator.SetBool("isAttack", false);
            return;
        }
        base.Attack();
    }
}