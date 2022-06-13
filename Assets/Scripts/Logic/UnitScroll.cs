using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitScroll : MonoBehaviour
{
    [SerializeField] private ScrollRect _unitScroller;

    private void OnEnable()
    {
        Events.OnMoveEnd += TurnOffScroll;
        Events.OnMoveStart += TurnOnScroll;
    }

    private void OnDisable()
    {
        Events.OnMoveEnd -= TurnOffScroll;
        Events.OnMoveStart -= TurnOnScroll;
    }

    private void TurnOnScroll()
    {
        _unitScroller.horizontal = true;
    }

    private void TurnOffScroll()
    {
        _unitScroller.horizontal = false;
    }


}
