using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitHealthBar : MonoBehaviour
{
    [SerializeField] Unit _unit;
    [SerializeField] Slider _healthSlider;

    private void OnEnable()
    {
        _unit.OnGetDamage += UpdateHealth;
    }

    private void OnDisable()
    {
        _unit.OnGetDamage -= UpdateHealth;
    }

    private void Start()
    {
        _healthSlider.maxValue = _unit.GetHealth();
        _healthSlider.value = _unit.GetHealth();
    }

    private void UpdateHealth()
    {
        _healthSlider.value = _unit.GetHealth();
    }
}
