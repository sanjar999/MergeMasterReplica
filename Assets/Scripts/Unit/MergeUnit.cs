using UnityEngine;

public class MergeUnit : MonoBehaviour
{
    [SerializeField] UnitSpawner _unitSpawner;

    public bool MergeTwoUnit(Unit unit_1, Unit unit_2)
    {
        if (unit_1.GetLevel() != unit_2.GetLevel() ||
            !unit_2.CompareTag(unit_1.tag) || unit_1 == unit_2 ||
             unit_1.GetType() != unit_2.GetType())
            return false;

        unit_1.LevelUp();
        unit_2.ClearUnitTile();
        _unitSpawner.GetUnits().Remove(unit_2);
        Destroy(unit_2.gameObject);
        return true;
    }
}