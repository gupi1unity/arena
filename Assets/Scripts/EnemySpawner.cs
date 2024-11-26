using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner
{
    private EnemyFactory _enemyFactory;
    private ObservableList<Enemy> _enemies;
    private MonoBehaviour _context;
    private Queue<Vector3> _spawnPoints;
    private Vector3 _currentPoint;
    private bool _isSpawning;

    public EnemySpawner(EnemyFactory enemyFactory, ObservableList<Enemy> enemies, MonoBehaviour context, Queue<Vector3> spawnPoints)
    {
        _enemyFactory = enemyFactory;
        _enemies = enemies;
        _context = context;
        _spawnPoints = spawnPoints;

        _currentPoint = _spawnPoints.Dequeue();
    }

    public void StartSpawner()
    {
        _isSpawning = true;
        _context.StartCoroutine(SpawnEnemy());
    }

    public void StopSpawner()
    {
        _isSpawning = false;
    }

    private IEnumerator SpawnEnemy()
    {
        while (_isSpawning == true)
        {
            var enemy = _enemyFactory.Spawn(_currentPoint);
            _enemies.Add(enemy);

            _spawnPoints.Enqueue(_currentPoint);
            _currentPoint = _spawnPoints.Dequeue();

            yield return new WaitForSeconds(1);
        }
    }
}
