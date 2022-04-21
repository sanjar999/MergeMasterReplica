using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileOutline : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Tile"))
            if (GetComponentInParent<Unit>().IsFlying)
            {
                var _mat = other.GetComponent<MeshRenderer>().material;
                if (other.GetComponent<Tile>().HasUnit() && other.GetComponent<Tile>().GetCreature().gameObject != this.transform.parent.gameObject)
                    _mat.SetColor("_Color", new Color(1, 0, 0, 0.5f));
                else
                    _mat.SetColor("_Color", new Color(0, 1, 0, 0.5f));
            }
            else
            {
                var _mat = other.GetComponent<MeshRenderer>().material;
                _mat.SetColor("_Color", new Color(0, 1, 0, 0));
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
