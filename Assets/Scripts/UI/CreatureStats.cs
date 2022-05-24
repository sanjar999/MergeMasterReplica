using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CreatureStats : MonoBehaviour
{
    [SerializeField] Canvas _canvas;
    [SerializeField] TMP_Text _levelNum;
    [SerializeField] Slider _healthSlider;
    [SerializeField] Creature _creature;


    private void OnEnable()
    {
        Events.OnLevelUp += UpdateLevel;
        Events.OnGetDamage += UpdateHealth;
    }

    private void OnDisable()
    {
        Events.OnLevelUp -= UpdateLevel;
        Events.OnGetDamage -= UpdateHealth;
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
        _levelNum.text = _creature.GetLevel().ToString();
    }
    private void UpdateHealth()
    {
        _healthSlider.value = _creature.GetHealth();
    }
} 