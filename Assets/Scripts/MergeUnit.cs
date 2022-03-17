using UnityEngine;

public class MergeUnit : MonoBehaviour
{
    [SerializeField] Board _board;

    public bool MergeTwoUnit(Unit unit_1, Unit unit_2)
    {
        if ( unit_1.Level != unit_2.Level ||
            !unit_2.CompareTag(unit_1.tag) ||
             unit_1 == unit_2)
            return false;

        _board.SetObjectToBoard(unit_2.x, unit_2.y, null);
        _board.RemoveUnit(unit_2);

        unit_2.gameObject.SetActive(false);
        //Destroy(unit_2);
        unit_1.Level++;
        return true;
    }

}
