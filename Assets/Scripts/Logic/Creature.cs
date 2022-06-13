using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Creature : MonoBehaviour
{
    [SerializeField] protected Animator _animator;
    [SerializeField] protected float _health = 80;
    [SerializeField] protected float _damage = 2;

    protected NavMeshAgent _agent;
    protected Creature _target;
    protected int _level = 1;
    protected bool _isFight;

    public float GetHealth() => _health;
    public int GetLevel() => _level;
    public virtual void SetLevel(int level) { _level = level; }

    protected float damageOffset = 0;
    protected List<Creature> _enemies;



    public Creature GetClosestEnemy(List<Creature> enemies)
    {
        float distance = float.MaxValue;
        Creature result = null;
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

    protected virtual void DealDamage()
    {
        _target.GetComponent<Creature>().GetDamage(_damage * _level);
        Events.OnDealDamage?.Invoke();

    }
    public virtual void GetDamage(float amount)
    {
        _health -= amount;
        Events.OnGetDamage?.Invoke();
        if (_health <= 0)
        {
            _agent.enabled = false;
            _animator.SetBool("isDead", true);
            Destroy(gameObject.GetComponent<Creature>());
        }
    }

    public virtual void LevelUp() { _level++; Events.OnLevelUp?.Invoke(); }

    protected virtual void Attack()
    {
        damageOffset += Time.deltaTime;
        if (!_target)
            _target = GetClosestEnemy(_enemies);
        else
        {
            _agent.SetDestination(_target.transform.position);

            transform.LookAt(new Vector3(_target.transform.position.x, transform.position.y, _target.transform.position.z));

            _agent.obstacleAvoidanceType = ObstacleAvoidanceType.HighQualityObstacleAvoidance;
            if (_agent.remainingDistance <= _agent.stoppingDistance && damageOffset > .3f)
            {
                damageOffset = 0;
                DealDamage();
            }
        }
        if (_target && transform)
            transform.LookAt(new Vector3(_target.transform.position.x, transform.position.y, _target.transform.position.z));
    }
    public void StartFightAnim() { _animator.SetBool("isAttack", true); }
}