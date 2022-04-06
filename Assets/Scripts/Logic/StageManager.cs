using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public int GetCurrentStage() => PlayerPrefs.GetInt("stage", 1);
}
