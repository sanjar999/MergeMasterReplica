using UnityEngine;

public class StageManager : MonoBehaviour
{
    private void OnEnable()
    {
        Events.OnWin += StageUp;
    }

    private void OnDisable()
    {
        Events.OnWin -= StageUp;
    }

    public int GetCurrentStage() => PlayerPrefs.GetInt("stage", 1);

    private void StageUp()
    {
        print("STAGE UP");
        var stage = PlayerPrefs.GetInt("stage", 1);
        stage++;
        PlayerPrefs.SetInt("stage", stage);
    }
}
