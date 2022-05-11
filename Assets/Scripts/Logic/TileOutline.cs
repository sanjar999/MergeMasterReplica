using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileOutline : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {

        bool isMergeble = false;


        if (other.CompareTag("Tile"))
        {
            Creature unit_1 = transform.parent.gameObject.GetComponent<Unit>();
            Creature unit_2 = other.GetComponent<Tile>().GetCreature();
            var _mat = other.GetComponent<MeshRenderer>().material;

            //print('1');
            //print(unit_1);
            //print('2');
            //print(unit_2);
            if (unit_1 != null && unit_2 != null)
                if (
                    unit_1.GetLevel() == unit_2.GetLevel() &&
                    unit_2.CompareTag(unit_1.tag) && unit_1 != unit_2 &&
                    unit_1.GetType() == unit_2.GetType())
                {
                    isMergeble = true;
                }

            if (GetComponentInParent<Unit>().IsFlying)
            {


                if (other.GetComponent<Tile>().HasUnit()
                    && other.GetComponent<Tile>().GetCreature().gameObject != this.transform.parent.gameObject
                    && !isMergeble
                    )
                {
                    _mat.SetColor("_Color", new Color(1, 0, 0, 0.5f));
                }
                else if (isMergeble)
                {
                    _mat.SetColor("_Color", new Color(0, 0, 1, 0.5f));
                }
                else
                {
                    _mat.SetColor("_Color", new Color(0, 1, 0, 0.5f));
                }
            }
            else
            {
                //var _mat = other.GetComponent<MeshRenderer>().material;
                _mat.SetColor("_Color", new Color(0, 1, 0, 0));
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Tile"))
            if (GetComponentInParent<Unit>().IsFlying)
            {
                var _mat = other.GetComponent<MeshRenderer>().material;
                _mat.SetColor("_Color", new Color(0, 1, 0, 0));

            }
    }
}
