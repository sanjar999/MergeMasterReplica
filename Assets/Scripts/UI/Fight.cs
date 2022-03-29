using System;
using UnityEngine;
using UnityEngine.UI;

public class Fight : MonoBehaviour
{
    [SerializeField] Button _fightBtn;
    [SerializeField] Board _board;

    void Start()
    {
        _fightBtn.onClick.AddListener(StartFight);
    }

    private void StartFight()
    {
        OnFight?.Invoke();
        foreach (var unit in _board.GetUnits())
        {
            unit.StartFightAnim();

        }
    }

    public Action OnFight;
}
