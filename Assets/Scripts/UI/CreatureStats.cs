using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CreatureStats : MonoBehaviour
{
    [SerializeField] Canvas _canvas;
    [SerializeField] TMP_Text _levelNum;
    [SerializeField] Slider _healthSlider;

    private void Start()
    {
        _canvas.worldCamera = Camera.main;
    }
    public void SetHealthSlider(float amount)
    {
        _healthSlider.maxValue = amount;
        _healthSlider.value = amount;
    }
    void Update()
    {
        _canvas.transform.rotation = Quaternion.identity;
    }
    public void UpdateLevel(string level)
    {
        _levelNum.text = level;
    }
    public void UpdateHealth(float amount)
    {
        _healthSlider.value = amount;
    }
} 