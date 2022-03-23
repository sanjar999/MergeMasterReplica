using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected float _health = 100;

    public float GetHealth() => _health;

    public void GetDamage(float amount)
    {
        _health -= amount;
        OnGetDamage?.Invoke();
        if (_health<=0)
        {
            Destroy(gameObject);
        }

    }

    public Action OnGetDamage;
    public Action OnDealDamage;
}
