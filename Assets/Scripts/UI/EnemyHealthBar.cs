using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    [SerializeField] Enemy _enemy;
    [SerializeField] Slider _healthSlider;

    private void OnEnable()
    {
            _enemy.OnGetDamage += UpdateHealth;
    }

    private void OnDisable()
    {
            _enemy.OnGetDamage -= UpdateHealth;
    }

    private void Start()
    {
        _healthSlider.maxValue = _enemy.GetHealth();
        _healthSlider.value = _enemy.GetHealth();
    }

    private void UpdateHealth()
    {
        _healthSlider.value = _enemy.GetHealth();
    }
}