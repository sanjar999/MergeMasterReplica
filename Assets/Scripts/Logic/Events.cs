using System;
using UnityEngine;

public class Events : MonoBehaviour
{
    public static Action OnDealDamage;
    public static Action OnGetDamage;
    public static Action OnMoveStart;
    public static Action OnLevelUp;
    public static Action OnMoveEnd;
    public static Action OnSpawn;
    public static Action OnFight;
    public static Action OnLose;
    public static Action OnWin;
}