using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCanvas : MonoBehaviour
{
    [SerializeField] Canvas _canvas;

    void Update()
    {
        _canvas.transform.rotation = Quaternion.identity;
    }
}
