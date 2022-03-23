using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthIndicator : MonoBehaviour
{
    [SerializeField] TMP_Text _healthValue;
    [SerializeField] Enemy _enemy;
    [SerializeField] Unit _unit;

    private void OnEnable()
    {
        if (_enemy)
        {
            _enemy.OnGetDamage += UpdateHealth;
        }
        else
        {
            _unit.OnGetDamage += UpdateHealth;
        }
    }

    private void OnDisable()
    {
        if (_enemy)
        {
            _enemy.OnGetDamage -= UpdateHealth;
        }
        else
        {
            _unit.OnGetDamage -= UpdateHealth;
        }
    }

    private void Start()
    {
        if (_enemy)
        {
            _healthValue.text = _enemy.GetHealth().ToString();
        }
        else
        {
            _healthValue.text = _unit.GetHealth().ToString();
        }
    }

    private void UpdateHealth()
    {
        if (_enemy)
        {
            _healthValue.text = _enemy.GetHealth().ToString();
        }
        else
        {
            _healthValue.text = _unit.GetHealth().ToString();
        }
    }
}