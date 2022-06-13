using UnityEngine;

public class MergeUnit : MonoBehaviour
{
    [SerializeField] UnitSpawner _unitSpawner;

    public bool TryMergeTwoUnit(Unit unit_1, Unit unit_2)
    {
        if (unit_1.GetLevel() != unit_2.GetLevel() || unit_1 == unit_2 ||
             unit_1.GetUnitType() != unit_2.GetUnitType())
            return false;

        unit_1.LevelUp();
        unit_2.TryClearUnitTile();
        _unitSpawner.GetUnits().Remove(unit_2);
        Destroy(unit_2.gameObject);
        return true;
    }
}