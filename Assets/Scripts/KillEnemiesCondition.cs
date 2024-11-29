using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillEnemiesCondition : IGameCondition
{
    public event Action ConditionChanged;

    private ObservableList<Enemy> _enemies;
    private int _enemyCount;

    private bool _isRunning = false;

    public KillEnemiesCondition(ObservableList<Enemy> enemies)
    {
        _enemies = enemies;
        _enemyCount = 0;

        _enemies.Added += OnEnemyAdded;
    }

    public void OnEnemyAdded(Enemy enemy)
    {
        _enemyCount += 1;
        enemy.EnemyDied += OnEnemyDied;
        _isRunning = true;
    }

    public void OnEnemyDied()
    {
        _enemyCount -= 1;
    }

    public void Update()
    {
        if (_enemyCount <= 0 && _isRunning == true)
        {
            ConditionChanged?.Invoke();
            Disable();
        }
    }

    private void Disable()
    {
        foreach (var enemy in _enemies.List)
        {
            enemy.EnemyDied -= OnEnemyDied;
        }

        _enemies.Added -= OnEnemyAdded;
    }
}
