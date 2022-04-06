using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI : MonoBehaviour
{
    [SerializeField] TMP_Text _stageNumber;
    [SerializeField] TMP_Text _fps;
    [SerializeField] EnemySpawner _enemySpawner;
    [SerializeField, Range(0.1f, 2f)]
    float sampleDuration = 1f;
    int frames;
    float duration;

    private bool _isCalled;

    private void OnEnable()
    {
        _enemySpawner.OnWin += StageUp;
    }

    private void OnDisable()
    {
        _enemySpawner.OnWin -= StageUp;
    }

    private void Start()
    {
        var stage = PlayerPrefs.GetInt("stage", 1);
        _stageNumber.text = stage.ToString();

    }

    private void Update()
    {
        float frameDuration = Time.unscaledDeltaTime;
        frames += 1;
        duration += frameDuration;
        if (duration >= sampleDuration)
        {
            _fps.SetText("FPS\n{0:0}", frames / duration);
            frames = 0;
            duration = 0f;
        }
    }

    private void StageUp()
    {
        if (!_isCalled)
        {
            _isCalled = true;
            print("call");
            var stage = PlayerPrefs.GetInt("stage", 1);
            stage++;
            PlayerPrefs.SetInt("stage", stage);
            _stageNumber.text = stage.ToString();
        }
    }
}
