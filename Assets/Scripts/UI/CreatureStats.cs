using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CreatureStats : MonoBehaviour
{
    [SerializeField] Canvas _canvas;
    [SerializeField] TMP_Text _levelText;
    [SerializeField] Slider _healthSlider;
    [SerializeField] Creature _creature;


    private void OnEnable()
    {
        _creature.OnLevelUp += UpdateLevel;
        _creature.OnGetDamage += UpdateHealth;

    }

    private void OnDisable()
    {
        _creature.OnLevelUp -= UpdateLevel;
        _creature.OnGetDamage -= UpdateHealth;

    }

    private void Start()
    {
        _canvas.worldCamera = Camera.main;
        _healthSlider.maxValue = _creature.GetHealth();
        _healthSlider.value = _creature.GetHealth();
        UpdateLevel();
    }
    void Update()
    {
        _canvas.transform.rotation = Quaternion.identity;
    }
    private void UpdateLevel()
    {
        _levelText.text = _creature.GetLevel().ToString();
    }
    private void UpdateHealth()
    {
        _healthSlider.value = _creature.GetHealth();
    }
} 