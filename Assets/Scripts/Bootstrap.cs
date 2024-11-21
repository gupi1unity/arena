using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] private Transform _playerSpawnPoint;
    private PlayerController _playerController;

    [SerializeField] private EnemyFactory _enemyFactory;
    [SerializeField] private List<Transform> _spawnPointsTransform;
    [SerializeField] private BulletFactory _bulletFactory;
    private List<Vector3> _spawnPoints;
    private List<Enemy> _enemies;

    [SerializeField] private WinConditions _winConditions;
    [SerializeField] private LoseConditions _loseConditions;

    [SerializeField] private float _timeToWin;

    [SerializeField] private ConditionController _conditionController;

    [SerializeField] private int _enemyCountToLose;

    private void Awake()
    {
        _conditionController.Initialize(_winConditions, _loseConditions, _timeToWin, _enemies, _playerController, _enemyCountToLose);

        _spawnPoints = _spawnPointsTransform.Select(spawnPoint => spawnPoint.position).ToList();

        _enemyFactory.Initialize();
        _bulletFactory.Initialize();

        GameObject playerPrefab = Instantiate(_playerPrefab, _playerSpawnPoint.position, Quaternion.identity);
        _playerController = playerPrefab.GetComponent<PlayerController>();
        _playerController.Initialize(_bulletFactory);
        

        StartCoroutine(SpawnEnemy());
    }

    private IEnumerator SpawnEnemy()
    {
        foreach (Vector3 spawnPoint in _spawnPoints)
        {
            var enemy = _enemyFactory.Spawn(spawnPoint);
            _enemies.Add(enemy);
            yield return new WaitForSeconds(1);
        }
    }
}
