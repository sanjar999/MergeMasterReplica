using UnityEngine;

public class FPSNoRestriction : MonoBehaviour
{
    void Start()
    {
        Application.targetFrameRate = 300;
    }
}
