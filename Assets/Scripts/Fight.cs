using System;
using UnityEngine;
using UnityEngine.UI;

public class Fight : MonoBehaviour
{
    [SerializeField] Button _fightBtn;
    [SerializeField] GameObject _tempTarget;

    void Start()
    {
        _fightBtn.onClick.AddListener(() => OnFight?.Invoke(_tempTarget));
    }

    public Action<GameObject> OnFight;
}
