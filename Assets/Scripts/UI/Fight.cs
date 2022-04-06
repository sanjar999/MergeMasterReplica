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
        _fightBtn.onClick.AddListener(StartFight);
    }

    private void StartFight()
    {
        OnFight?.Invoke();
        foreach (var unit in _unitSpawner.GetUnits())
        {
            unit.StartFightAnim();
        }
        foreach (var enemy in _enemySpawner.GetEnemies())
        {
            enemy.StartFightAnim();
        }
    }

    public Action OnFight;
}
