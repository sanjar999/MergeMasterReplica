using TMPro;
using UnityEngine;

public class UI : MonoBehaviour
{
    [SerializeField] TMP_Text _stageNumber;
    [SerializeField] TMP_Text _fps;
    [SerializeField] StageManager _stageManager;
    [SerializeField, Range(0.1f, 2f)] float sampleDuration = 1f;
    int frames;
    float duration;


    private void OnEnable()
    {
        Events.OnWin += UpdateStageText;
    }

    private void OnDisable()
    {
        Events.OnWin -= UpdateStageText;
    }

    private void Start()
    {
        var stage = PlayerPrefs.GetInt("stage", 1);
        _stageNumber.text = stage.ToString();

    }

    private void Update()
    {
        CalculateFps();
    }

    private void CalculateFps()
    {
        float frameDuration = Time.unscaledDeltaTime;
        frames += 1;
        duration += frameDuration;
        if (duration >= sampleDuration)
        {
            _fps.SetText("FPS:{0:0}", frames / duration);
            frames = 0;
            duration = 0f;
        }
    }

    private void UpdateStageText()
    {
        _stageNumber.text = _stageManager.GetCurrentStage().ToString();
    }
}