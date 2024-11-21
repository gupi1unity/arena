using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter
{
    private float _speed;
    private BulletFactory _bulletFactory;
    private Transform _playerTransform;

    public Shooter(float speed, BulletFactory bulletFactory, Transform playerTransform)
    {
        _speed = speed;
        _bulletFactory = bulletFactory;
        _playerTransform = playerTransform;
    }

    public void Shoot(Vector3 shootPoint)
    {
        var bullet = _bulletFactory.Spawn(shootPoint);
        bullet.gameObject.GetComponent<Rigidbody>().velocity = _playerTransform.forward * _speed;
    }
}
