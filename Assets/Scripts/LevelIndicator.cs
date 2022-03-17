using TMPro;
using UnityEngine;

public class LevelIndicator : MonoBehaviour
{
    [SerializeField] Canvas _canvas;
    [SerializeField] TMP_Text _levelText;
    [SerializeField] Unit _unit;


    private void OnEnable()
    {
        _unit.OnLevelUp += UpdateLevel;
    }

    private void OnDisable()
    {
        _unit.OnLevelUp -= UpdateLevel;
    }

    private void Start()
    {
        _canvas.worldCamera = Camera.main;
    }

    private void UpdateLevel()
    {
        _levelText.text = _unit.Level.ToString();
    }

}
