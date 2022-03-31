using System;
using UnityEngine;
using UnityEngine.UI;

public class Fight : MonoBehaviour
{
    [SerializeField] Button _fightBtn;
    [SerializeField] UnitSpawner _unitSpawner;

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
    }

    public Action OnFight;
}
