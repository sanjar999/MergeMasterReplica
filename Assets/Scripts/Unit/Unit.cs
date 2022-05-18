using System;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;

public class Unit : Creature
{
    [SerializeField] protected Transform _raycastPos;
    [SerializeField] protected float _defence = .5f;

    [SerializeField] protected float _lvlUpHpIncrease = 10f;
    [SerializeField] protected float _lvlUpDefIncrease = .1f;
    [SerializeField] protected float _lvlUpDmgIncrease = .4f;

    public enum UnitType { range, melee }

    protected Tile _unitTile;
    protected UnitType _unitType;
    protected EnemySpawner _enemySpawner;

    public bool IsDragging { get; set; }
    public void SetTile(Tile tile) { _unitTile = tile; }
    public Tile GetTile() => _unitTile;
    public void SetEnemySpawner(EnemySpawner enemySpawner) { _enemySpawner = enemySpawner; }
    public Vector3 GetRaycastPos() => _raycastPos.position;
    public UnitType GetUnitType() => _unitType;
    public void SetUnitType(UnitType unitType) { _unitType = unitType; }

    private void Start()
    {
        _fight.OnFight += () => _isFight = true; 
        _enemySpawner.OnSpawn += GetEnemies;
        _agent = GetComponent<NavMeshAgent>();
        GetEnemies();
    }

    private void OnDisable()
    {
        _fight.OnFight -= () => _isFight = true;
        _enemySpawner.OnSpawn -= GetEnemies;
    }

    private void Update()
    {
        if (_isFight)
            Attack();
        if (_animator)
            _animator.SetFloat("Blend", _agent.velocity.magnitude);
    }
    private void GetEnemies() { _enemies = _enemySpawner.GetEnemies().ConvertAll(new Converter<Enemy, Creature>(CreatureToUnit)); }

    protected override void Attack()
    {
        if (!_enemySpawner.GetHasEnemy())
        {
            _agent.isStopped = true;
            _animator.SetBool("isAttack", false);
            return;
        }
        base.Attack();
    }
    public override void SetLevel(int level)
    {
        base.SetLevel(level);
        _health = _level * _lvlUpHpIncrease;
        _damage = _level * _lvlUpDmgIncrease;
        _defence = _level * _lvlUpDefIncrease;
    }

    public override void LevelUp()
    {
        base.LevelUp();
        _health = _level * _lvlUpHpIncrease;
        _defence = _level * _lvlUpDefIncrease;
        _damage = _level * _lvlUpDmgIncrease;
    }

    public override void GetDamage(float amount)
    { 
        base.GetDamage(amount - _defence);
        _target = GetCloseEnemy(_enemies);
    }

    private Enemy CreatureToUnit(Creature c) => c as Enemy;
    public void ClearUnitTile() { _unitTile.SetCreature(null); }

    public void SetAgentSpeed(float speed)
    {
        float t = Math.Abs(speed-1);
        DOTween.To(() => t, x => t = x, speed, 0.2f);
        _agent.speed = t;
        print(t);
    }
}