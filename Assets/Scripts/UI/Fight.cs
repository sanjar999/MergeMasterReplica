using System;
using UnityEngine;
using UnityEngine.UI;

public class Fight : MonoBehaviour
{
    [SerializeField] Button _fightBtn;

    void Start()
    {
        _fightBtn.onClick.AddListener(() => OnFight?.Invoke());
    }

    public Action OnFight;
}
