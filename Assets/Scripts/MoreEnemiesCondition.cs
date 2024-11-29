using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoreEnemiesCondition : IGameCondition
{
    public event Action ConditionChanged;

    private int _enemyCountToLose;
    private ObservableList<Enemy> _enemies;
    private int _enemyCount;

    public MoreEnemiesCondition(int enemyCountToLose, ObservableList<Enemy> enemies)
    {
        _enemyCountToLose = enemyCountToLose;
        _enemies = enemies;
        _enemyCount = _enemies.List.Count;

        _enemies.Added += OnEnemyAdded;
    }

    public void OnEnemyAdded(Enemy enemy)
    {
        _enemyCount += 1;
        enemy.EnemyDied += OnEnemyDied;
    }

    public void OnEnemyDied()
    {
        _enemyCount -= 1;
    }

    public void Update()
    {
        if (_enemyCount >= _enemyCountToLose)
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
