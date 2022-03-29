using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Unit : MonoBehaviour
{
    [SerializeField] protected Animator _animator;
    [SerializeField] protected GameObject _mesh;
    public enum UnitType { range, melee }

    protected Vector2Int _coord;
    protected UnitType _unitType;
    protected NavMeshAgent _agent;
    protected Transform  _target;

    protected int _level = 1;
    protected float _health = 80;
    protected float _damage = 2;
    protected float _defence = 2;

    private bool _isFight;

    protected Fight _fight;
    protected EnemySpawner _enemySpawner;

    public void SetEnemySpawner(EnemySpawner enemySpawner) { _enemySpawner = enemySpawner; }
    public void SetFight(Fight fight) { _fight = fight; }

    public float GetHealth() => _health;
    public int GetLevel() => _level;
    public void SetLevel(int level) { _level = level; }
    public void LevelUp() { _level++; OnLevelUp?.Invoke(); }

    public Vector2Int GetCoord() => _coord;
    public void SetCoord(Vector2Int coord) { _coord = coord; }

    public UnitType GetUnitType() => _unitType;
    public void SetUnitType(UnitType unitType) { _unitType = unitType; }

    private float damageOffset = 0;
    private List<Enemy> _enemies;

    private void Start()
    {
        if (_fight)
            _fight.OnFight += () =>  _isFight = true; 

        _agent = GetComponent<NavMeshAgent>();
        _enemies = _enemySpawner.GetEnemies();


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
        _animator.SetFloat("Blend", _agent.velocity.magnitude);

    }

    public void StartFightAnim()
    {
        _animator.SetBool("isAttack", true);
    }

    protected virtual void Attack()
    {
        damageOffset += Time.deltaTime;

        if (!_enemySpawner.HasEnemy())
        {
            _agent.isStopped = true;
            _animator.SetBool("isAttack",false);
            return;
        }

        if (!_target)
        {
            _target = GetCloseEnemy(_enemies).gameObject.transform;
        }
        else
        {
            _agent.SetDestination(_target.position);
            transform.LookAt(new Vector3(_target.position.x, 0, _target.position.z));

            _agent.obstacleAvoidanceType = ObstacleAvoidanceType.HighQualityObstacleAvoidance;
            if (_agent.remainingDistance <= _agent.stoppingDistance && damageOffset > .3f)
            {
                damageOffset = 0;
                DealDamage(_target.GetComponent<Enemy>(), _damage * _level);

            }
        }
        transform.LookAt(new Vector3(_target.position.x, 0, _target.position.z));
    }

    public Enemy GetCloseEnemy(List<Enemy> enemies)
    {
        float distance = float.MaxValue;
        Enemy result = null;
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

    protected virtual void DealDamage(Enemy target, float amount)
    {
        target.GetDamage(amount);
        OnDealDamage?.Invoke();

    }
    public virtual void GetDamage(float amount)
    {
        _target = GetCloseEnemy(_enemies).gameObject.transform;

        _health -= amount;
        OnGetDamage?.Invoke();
        if (_health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public Action OnGetDamage;
    public Action OnDealDamage;
    public Action OnLevelUp;

}