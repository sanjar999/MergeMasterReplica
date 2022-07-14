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
    [SerializeField] private bool _isFromScroller;

    protected Tile _unitTile;
    protected UnitType _unitType;
    protected EnemySpawner _enemySpawner;
    protected bool _isWin;

    public enum UnitType { reddy, greenSpy, scorpy, lazer, pyro, cryo , length }
    public bool IsDragging { get; set; }
    public bool IsFromScroller() => _isFromScroller;
    public void SetTile(Tile tile) { _unitTile = tile; }
    public Tile GetTile() => _unitTile;
    public void SetEnemySpawner(EnemySpawner enemySpawner) { _enemySpawner = enemySpawner; }
    public Vector3 GetRaycastPos() => _raycastPos.position;
    public UnitType GetUnitType() => _unitType;
    public void SetUnitType(UnitType unitType) { _unitType = unitType; }

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
        if (_animator)
            _animator.SetFloat("Blend", _agent.velocity.magnitude);
    }
    private void GetEnemies() { _enemies = _enemySpawner.GetEnemies().ConvertAll(new Converter<Enemy, Creature>(CreatureToUnit)); }

    protected override void Attack()
    {
        if (!_enemySpawner.HasEnemy&& !_isWin)
        {
            _isWin = true;
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
        _target = GetClosestEnemy(_enemies);
    }

    private Enemy CreatureToUnit(Creature c) => c as Enemy;
    public void TryClearUnitTile()
    {
        if (_unitTile)
            _unitTile.SetCreature(null);
    }

    public void SetAgentSpeed(float speed)
    {
        float t = Math.Abs(speed-1);
        DOTween.To(() => t, x => t = x, speed, 0.2f);
        _agent.speed = t;
    }
}