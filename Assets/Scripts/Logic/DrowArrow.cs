using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrowArrow : MonoBehaviour
{
    [SerializeField] GameObject _arrow;
    [SerializeField] Transform _shootPos;
    private List<GameObject> _arrows = new List<GameObject>();
    private int _arrowIndex;

    private void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            _arrows.Add(Instantiate(_arrow));
        }
    }


    public void EnableArrow() { _arrow.SetActive(true); }
    public void DisableArrow() { _arrow.SetActive(false); DrawArrow(); }

    private void DrawArrow()
    {
        _arrowIndex++;
        var arrow = Instantiate(_arrow, _shootPos.position, Quaternion.identity);
        arrow.SetActive(true);
        arrow.transform.localRotation = Quaternion.Euler(90, 0, 0);
        var rb = arrow.GetComponent<Rigidbody>();
        rb.isKinematic = false;
        rb.AddForce(Vector3.forward * 100);
    }
}

