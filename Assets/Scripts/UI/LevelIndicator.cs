using TMPro;
using UnityEngine;

public class LevelIndicator : MonoBehaviour
{
    [SerializeField] Canvas _canvas;
    [SerializeField] TMP_Text _levelText;
    [SerializeField] Creature _creature;


    private void OnEnable()
    {
        _creature.OnLevelUp += UpdateLevel;
    }

    private void OnDisable()
    {
        _creature.OnLevelUp -= UpdateLevel;
    }

    private void Start()
    {
        _canvas.worldCamera = Camera.main;
        UpdateLevel();
    }

    private void UpdateLevel()
    {
        _levelText.text = _creature.GetLevel().ToString();
    }
}