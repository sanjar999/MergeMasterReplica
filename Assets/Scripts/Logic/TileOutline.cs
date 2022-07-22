using UnityEngine;

public class TileOutline : MonoBehaviour
{
    private Material _mat;

    private void OnTriggerStay(Collider other)
    {
        if (GetComponentInParent<Unit>())
            if (other.CompareTag("Tile") && GetComponentInParent<Unit>().IsDragging)
            {
                _mat = other.GetComponent<MeshRenderer>().material;
                var tile = other.GetComponent<Tile>();

                if (tile.HasUnit() && tile.GetCreature().gameObject != transform.parent.gameObject)
                    _mat.SetColor("_BaseColor", Color.red);
                else
                    _mat.SetColor("_BaseColor", Color.green);
            }
    }

    private void OnTriggerExit(Collider other)
    {
        if (GetComponentInParent<Unit>())
            if (other.CompareTag("Tile") && GetComponentInParent<Unit>().IsDragging)
                _mat.SetColor("_BaseColor", Color.clear);
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(0) && _mat != null)
            _mat.SetColor("_BaseColor", Color.clear);
    }
}
