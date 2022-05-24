using System.Collections.Generic;
using UnityEngine;

public class GameProgress : MonoBehaviour
{
    [SerializeField] UnitSpawner _unitSpawner;
    [SerializeField] UnitMove _unitMove;
    private List<Unit> _units = new List<Unit>();

    private void Start()
    {
        _units = _unitSpawner.GetUnits();
    }
    private void OnEnable()
    {
        Events.OnSpawn += SaveData;
        Events.OnMove += SaveData;
    }
    private void OnDisable()
    {
        Events.OnSpawn -= SaveData;
        Events.OnMove -= SaveData;
    }

    private void SaveData()
    {
        PlayerPrefs.SetInt("units_count", _units.Count);

        for (int i = 0; i < _units.Count; i++)
        {
            var tile = _units[i].GetTile();
            PlayerPrefs.SetInt($"units_{i}_level", _units[i].GetLevel());
            PlayerPrefs.SetInt($"unitTile_{i}_x", tile.GetCoord().x);
            PlayerPrefs.SetInt($"unitTile_{i}_y", tile.GetCoord().y);
            PlayerPrefs.SetInt($"units_{i}_type", (int)_units[i].GetUnitType());
        }
    }
}
