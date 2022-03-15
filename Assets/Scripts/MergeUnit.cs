using UnityEngine;

public class MergeUnit : MonoBehaviour
{
    [SerializeField] Board _board;

    public bool MergeTwoUnit(Unit unit_1, Unit unit_2)
    {
        if (unit_1.Level != unit_2.Level)
            return false;

        print("merge");
        _board.SetObjectToBoard(unit_2.x, unit_2.y, null);
        unit_2.gameObject.SetActive(false);
        unit_1.GetComponent<Renderer>().material.SetColor("_Color", Color.green);
        unit_1.Level++;
        return true;
    }

}
