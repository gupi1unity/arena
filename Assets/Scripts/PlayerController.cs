using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IDamagable
{
    public event Action PlayerDied;

    [SerializeField] private float _speed;
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private int _healthValue;

    [SerializeField] private Transform _shootPoint;
    [SerializeField] private float _bulletSpeed = 5f;
    private BulletFactory _bulletFactory;

    private Mover _mover;
    private Shooter _shooter;
    private Rotator _rotator;
    private Health _health;

    public void Initialize(BulletFactory bulletFactory)
    {
        _bulletFactory = bulletFactory;

        _mover = new Mover(_speed, _characterController);
        _shooter = new Shooter(_bulletSpeed, _bulletFactory, transform);
        _rotator = new Rotator(transform);
        _health = new Health(_healthValue);
    }

    private void Update()
    {
        if (_health.HealthValue <= 0)
        {
            PlayerDied?.Invoke();
            Destroy(gameObject);
        }

        _mover.Move();
        
        if (_mover.MoveDirection.magnitude > 0.1f)
        {
            _rotator.Rotate(_mover.MoveDirection);
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            _shooter.Shoot(_shootPoint.position);
        }
    }

    public void TakeDamage(int damage)
    {
        _health.RemoveHealth(damage);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Enemy>(out Enemy enemy))
        {
            TakeDamage(enemy.Damage);
        }
    }
}
