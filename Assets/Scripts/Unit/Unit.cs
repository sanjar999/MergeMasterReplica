using System;
using UnityEngine;
using UnityEngine.AI;

public class Unit : Creature
{
    [SerializeField] protected Animator _animator;
    [SerializeField] protected Transform _raycastPos;
    public enum UnitType { range, melee }

    protected Tile _unitTile;
    protected UnitType _unitType;
    protected float _defence = 2;
    protected EnemySpawner _enemySpawner;

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
        _animator.SetFloat("Blend", _agent.velocity.magnitude);
    }
    private void GetEnemies() { _enemies = _enemySpawner.GetEnemies().ConvertAll(new Converter<Enemy, Creature>(CreatureToUnit)); }

    protected override void Attack()
    {
        if (!_enemySpawner.HasEnemy)
        {
            _agent.isStopped = true;
            _animator.SetBool("isAttack", false);
            return;
        }
        base.Attack();
    }

    public override void GetDamage(float amount)
    {
        base.GetDamage(amount);
        _target = GetCloseEnemy(_enemies).gameObject.transform;
    }

    private Enemy CreatureToUnit(Creature c) => c as Enemy;
    public void StartFightAnim() { _animator.SetBool("isAttack", true); }
    public void ClearUnitTile() { _unitTile.SetCreature(null); }
}