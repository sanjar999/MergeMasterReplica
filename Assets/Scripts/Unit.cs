using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public int x;
    public int y;

    private int level = 1;

    public int Level
    {
        get => level;
        set
        {
            level = value;
            OnLevelUp?.Invoke();
        }
    }

    public Action OnLevelUp;
}
