using System;
using UnityEngine;
using UnityEngine.UI;

public class Fight : MonoBehaviour
{
    [SerializeField] Button _fightBtn;
    [SerializeField] UnitSpawner _unitSpawner;
    [SerializeField] EnemySpawner _enemySpawner;

    void Start()
    {
        _fightBtn.onClick.AddListener(() => Events.OnFight?.Invoke());
    }

}
