using System.Collections.Generic;
using UnityEngine;

public class GameProgress : MonoBehaviour
{
    [SerializeField] UnitSpawner _unitSpawner;
    private List<Unit> _units = new();

    private void Start()
    {
        _units = _unitSpawner.GetUnits();
    }
    private void OnEnable()
    {
        Events.OnSpawn += SaveData;
        Events.OnMoveEnd += SaveData;
    }
    private void OnDisable()
    {
        Events.OnSpawn -= SaveData;
        Events.OnMoveEnd -= SaveData;
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
    public void SetCoins(int amount)
    {
        PlayerPrefs.SetInt("coins", amount);
    }
    public void SetUnitPrice(int amount)
    {
        PlayerPrefs.SetInt("unit_price", amount);
    }

    public int GetSvedUnitsCount() => PlayerPrefs.GetInt("units_count", 0);
    public int GetSvedUnitLevel(int index) => PlayerPrefs.GetInt ($"units_{index}_level", 0);
    public int GetSvedUnitIndexX(int index) => PlayerPrefs.GetInt($"unitTile_{index}_x",  0);
    public int GetSvedUnitIndexY(int index) => PlayerPrefs.GetInt($"unitTile_{index}_y",  0);
    public int GetSvedUnitType(int index) => PlayerPrefs.GetInt  ($"units_{index}_type", 0);
    public int GetCoins() => PlayerPrefs.GetInt("coins", 100);
    public int GetUnitPrice() => PlayerPrefs.GetInt("unit_price", 100);

}