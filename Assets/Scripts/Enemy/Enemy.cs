using System;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Creature
{
    [SerializeField] protected float _lvlUpHpIncrease = 10f;
    [SerializeField] protected float _lvlUpDmgIncrease = .4f;

    protected EnemyType _enemyType;
    protected UnitSpawner _unitSpawner;
    protected bool _isLose;

    public enum EnemyType { range, melee }
    public void SetUnitSpawner(UnitSpawner unitSpawner) { _unitSpawner = unitSpawner; }
    public EnemyType GetUnitType() => _enemyType;
    public void SetUnitType(EnemyType enemyType) { _enemyType = enemyType; }

    float sampleDuration = .5f;
    float duration;

    private void Start()
    {
        Events.OnFight += () => _isFight = true;
        Events.OnFight += StartFightAnim;
        Events.OnSpawn += GetEnemies;

        _agent = GetComponent<NavMeshAgent>();
        GetEnemies();
    }

    private void OnDisable()
    {
        Events.OnFight -= () => _isFight = true;
        Events.OnFight -= StartFightAnim;
        Events.OnSpawn -= GetEnemies;

    }

    private void Update()
    {
        float frameDuration = Time.unscaledDeltaTime;
        duration += frameDuration;
        if (_isFight && duration >= sampleDuration)
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
    protected override void Attack()
    {
        if (!_unitSpawner.HasUnit && !_isLose)
        {
            _isLose = true;
            _agent.isStopped = true;
            _animator.SetBool("isAttack", false);
            Events.OnLose?.Invoke();
            return;
        }
        base.Attack();
    }

    private Unit CreatureToUnit(Creature c) => c as Unit;
    private void GetEnemies() { _enemies = _unitSpawner.GetUnits().ConvertAll(new Converter<Unit, Creature>(CreatureToUnit)); }

}