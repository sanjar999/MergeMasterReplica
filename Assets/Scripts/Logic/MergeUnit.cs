using UnityEngine;

public class MergeUnit : MonoBehaviour
{
    [SerializeField] Board _board;

    public bool MergeTwoUnit(Unit unit_1, Unit unit_2)
    {
        if (unit_1.GetLevel() != unit_2.GetLevel() ||
            !unit_2.CompareTag(unit_1.tag) ||
             unit_1 == unit_2 ||
             unit_1.GetType() != unit_2.GetType())
            return false;

        var coord = unit_2.GetCoord();
        _board.SetObjectToBoard(coord.x, coord.y, null);
        _board.RemoveUnit(unit_2);
        unit_1.LevelUp();

        Destroy(unit_2.gameObject);
        return true;
    }

}
