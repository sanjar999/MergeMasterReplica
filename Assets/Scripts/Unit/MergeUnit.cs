using UnityEngine;

public class MergeUnit : MonoBehaviour
{
    [SerializeField] UnitSpawner _unitSpawner;

    public bool TryMergeTwoUnit(Unit unit_1, Unit unit_2)
    {
        if (unit_1 == unit_2 ||
             unit_1.GetUnitType() != unit_2.GetUnitType())
            return false;

        int sum = Mathf.Abs(unit_1.GetLevel() + unit_2.GetLevel());
        print(sum);
        unit_1.LevelUp(sum);
        unit_2.TryClearUnitTile();
        _unitSpawner.GetUnits().Remove(unit_2);
        Destroy(unit_2.gameObject);
        Events.OnMerge?.Invoke();
        return true;
    }
} 