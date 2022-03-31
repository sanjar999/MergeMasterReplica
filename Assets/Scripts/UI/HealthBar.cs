using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] Creature _creature;
    [SerializeField] Slider _healthSlider;

    private void OnEnable()
    {
        _creature.OnGetDamage += UpdateHealth;
    }

    private void OnDisable()
    {
        _creature.OnGetDamage -= UpdateHealth;
    }

    private void Start()
    {
        _healthSlider.maxValue = _creature.GetHealth();
        _healthSlider.value = _creature.GetHealth();
    }

    private void UpdateHealth()
    {
        _healthSlider.value = _creature.GetHealth();
    }
}
