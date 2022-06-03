using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [SerializeField] private Button _fightBtn;
    [SerializeField] private TMP_Text _stageNumber;
    [SerializeField] private StageManager _stageManager;

    private void Start()
    {
        _stageNumber.text = _stageManager.GetCurrentStage().ToString();
        _fightBtn.onClick.AddListener(() => Events.OnFight?.Invoke());
    }

    #region FPS
    [SerializeField] private TMP_Text _fps;
    [SerializeField, Range(0.1f, 2f)] private float sampleDuration = 1f;
    private int frames;
    private float duration;

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
    #endregion
}