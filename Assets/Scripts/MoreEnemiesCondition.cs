using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoreEnemiesCondition : IGameCondition
{
    public event Action ConditionChanged;

    private int _enemyCountToLose;
    private List<Enemy> _enemies;
    private int _enemyCount;

    public MoreEnemiesCondition(int enemyCountToLose, List<Enemy> enemies)
    {
        _enemyCountToLose = enemyCountToLose;
        _enemies = enemies;
        _enemyCount = _enemies.Count;

        foreach (Enemy enemy in enemies)
        {
            enemy.EnemyDied += OnEnemyDied;
        }
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
        }
    }
}
