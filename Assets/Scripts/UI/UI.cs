using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [SerializeField] private Button _fightBtn;
    [SerializeField] private TMP_Text _stageNumber;
    [SerializeField] private TMP_Text _fps;
    [SerializeField] private StageManager _stageManager;
    [SerializeField, Range(0.1f, 2f)] private float sampleDuration = 1f;

    private int frames;
    private float duration;

    //private void OnEnable()
    //{
    //    Events.OnWin += UpdateStageText;
    //}

    //private void OnDisable()
    //{
    //    Events.OnWin -= UpdateStageText;
    //}

    private void Start()
    {
        _stageNumber.text = _stageManager.GetCurrentStage().ToString();
        _fightBtn.onClick.AddListener(() => Events.OnFight?.Invoke());
        //UpdateStageText();
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

    //private void UpdateStageText()
    //{
    //    _stageNumber.text = _stageManager.GetCurrentStage().ToString();
    //}
}