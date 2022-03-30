using System.Collections.Generic;
using UnityEngine;

public class GameProgress : MonoBehaviour
{
    [SerializeField] Board _board;
    private List<Unit> _units;

    private void Start()
    {
        _units = _board.GetUnits();
    }

    private void OnDisable()
    {
        //SaveData();
    }

    private void SaveData()
    {
        PlayerPrefs.SetInt("units_count", _units.Count);

        for (int i = 0; i < _units.Count; i++)
        {
            var coord = _units[i].GetCoord();
            PlayerPrefs.SetInt($"units_{i}_level", _units[i].GetLevel());
            PlayerPrefs.SetInt($"units_{i}_x", coord.x);
            PlayerPrefs.SetInt($"units_{i}_y", coord.y);
            PlayerPrefs.SetInt($"units_{i}_type", (int)_units[i].GetUnitType());
        }
    }
}
