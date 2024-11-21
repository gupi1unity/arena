using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillEnemiesCondition : IGameCondition
{
    public event Action ConditionChanged;

    private List<Enemy> _enemies;
    private int _enemyCount;

    public KillEnemiesCondition(List<Enemy> enemies)
    {
        _enemies = enemies;

        _enemyCount = _enemies.Count;

        foreach (Enemy enemy in _enemies)
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
        if (_enemyCount <= 0)
        {
            ConditionChanged?.Invoke();
        }
    }
}
