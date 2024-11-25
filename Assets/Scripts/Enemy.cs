using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamagable
{
    public event Action EnemyDied;

    [SerializeField] private float _speed = 5;
    [SerializeField] private float _minValue = -10f;
    [SerializeField] private float _maxValue = 10f;
    [SerializeField] private int _healthValue;
    [SerializeField] private int _damage;
    private Vector3 _randomPoint;


    private Health _health;

    public int Damage { get => _damage; }

    private void Awake()
    {
        CreateRandomTarget();

        _health = new Health(_healthValue);
    }

    private void Update()
    {
        if (_health.HealthValue <= 0)
        {
            EnemyDied?.Invoke();
            gameObject.SetActive(false);
        }

        Vector3 distance = _randomPoint - transform.position;
        Vector3 moveDirection = new Vector3(distance.x, 0, distance.z);
        Vector3 moveDirectionNormalized = moveDirection.normalized;

        if (moveDirection.magnitude < 0.1f)
        {
            CreateRandomTarget();
        }

        transform.Translate(moveDirectionNormalized * _speed * Time.deltaTime);
    }

    private void CreateRandomTarget()
    {
        float randomX = UnityEngine.Random.Range(_minValue, _maxValue);
        float randomY = UnityEngine.Random.Range(_minValue, _maxValue);

        _randomPoint = new Vector3(randomX, 0, randomY);
    }

    public void TakeDamage(int damage)
    {
        _health.RemoveHealth(damage);
    }
}
