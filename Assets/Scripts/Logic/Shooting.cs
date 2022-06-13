using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField] private Transform _firstShootPos;
    [SerializeField] private Transform _secondShootPos;
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private GameObject _robot;
    [SerializeField] private float _bulletSpeed =100f;
    [SerializeField] private float _bulletsCount =100;
    [SerializeField] private float _shotOffset =.3f;
    private Queue<GameObject> _preservedBullets = new();
    private Queue<GameObject> _inUseBullets = new();

    float _timePassedFramLastShot;

    private void Start()
    {
        for (int i = 0; i < _bulletsCount; i++)
        {
            _preservedBullets.Enqueue(Instantiate(_bulletPrefab));
        }
    }

    private void Update()
    {
        _timePassedFramLastShot += Time.unscaledDeltaTime;
        if (_timePassedFramLastShot >= _shotOffset)
        {
            Shoot(_firstShootPos.position);
            Shoot(_secondShootPos.position);
            _timePassedFramLastShot = 0;
        }
    }

    private void Shoot(Vector3 shootingPos)
    {
        if (_preservedBullets.Count < 1)
        {
            _preservedBullets.Enqueue(_inUseBullets.Dequeue());
        }
        var bullet = _preservedBullets.Dequeue();
        var rb = bullet.GetComponent<Rigidbody>();
        var rot = new Vector3(bullet.transform.eulerAngles.x, _robot.transform.eulerAngles.y, bullet.transform.eulerAngles.z);
        bullet.transform.localEulerAngles = rot;
        bullet.transform.position = shootingPos;
        rb.velocity = bullet.transform.forward * _bulletSpeed;
        bullet.transform.parent = null;
        _inUseBullets.Enqueue(bullet);
    }
}