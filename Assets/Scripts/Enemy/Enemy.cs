using System;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Creature
{
    [SerializeField] protected float _lvlUpHpIncrease = 10f;
    [SerializeField] protected float _lvlUpDmgIncrease = .6f;

    protected EnemyType _enemyType;
    protected UnitSpawner _unitSpawner;
    protected bool _isLose;

    public enum EnemyType { range, melee }
    public void SetUnitSpawner(UnitSpawner unitSpawner) { _unitSpawner = unitSpawner; }
    public EnemyType GetUnitType() => _enemyType;
    public void SetUnitType(EnemyType enemyType) { _enemyType = enemyType; }

    float sampleDuration = .5f;
    float duration;
    private bool _fireParticlesIsOff;

    private void Start()
    {
        Events.OnFight += () => _isFight = true;
        Events.OnFight += StartFightAnim;
        Events.OnSpawn += GetEnemies;
        Events.OnWin += StopParticles;
        Events.OnLose += StopParticles;
        _creatureStats.SetHealthSlider(_health);
        _creatureStats.UpdateLevel(_level.ToString());
        _agent = GetComponent<NavMeshAgent>();
        GetEnemies();
    }

    private void OnDisable()
    {
        Events.OnFight -= () => _isFight = true;
        Events.OnFight -= StartFightAnim;
        Events.OnSpawn -= GetEnemies;
        Events.OnWin -= StopParticles;
        Events.OnLose -= StopParticles;

    }

    private void Update()
    {
        float frameDuration = Time.unscaledDeltaTime;
        duration += frameDuration;
        if (_isFight && duration >= sampleDuration)
            Attack();
        _animator.SetFloat("Blend", _agent.velocity.magnitude);

        if (_fireParticles)
            if (_isFight && _agent.velocity.magnitude == 0 && _fireParticlesIsOff && !_isLose)
            {
                _fireParticles.Play();
                _fireParticlesIsOff = false;
            }
            else
            {
                _fireParticles.Stop();
                _fireParticlesIsOff = true;
            }
    }

    public override void SetLevel(int level)
    {
        base.SetLevel(level);
        _health += _level * _lvlUpHpIncrease;
        _damage += _level * _lvlUpDmgIncrease;
    }

    public override void LevelUp(int sum)
    {
        base.LevelUp(sum);
        _health += _level * _lvlUpHpIncrease;
        _damage += _level * _lvlUpDmgIncrease;
    }
    protected override void Attack()
    {
        if (!_unitSpawner.HasUnit && !_isLose)
        {
            _isLose = true;
            _agent.isStopped = true;
            _animator.SetBool("isAttack", false);
            return;
        }
        base.Attack();
    }

    private Unit CreatureToUnit(Creature c) => c as Unit;
    private void GetEnemies()
    {
            _enemies = _unitSpawner.GetUnits().ConvertAll(new Converter<Unit, Creature>(CreatureToUnit));
    }

}