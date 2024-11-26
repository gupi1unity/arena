using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] private Transform _playerSpawnPoint;
    private PlayerController _playerController;

    private EnemySpawner _enemySpawner;
    [SerializeField] private EnemyFactory _enemyFactory;
    [SerializeField] private List<Transform> _spawnPointsTransform;
    [SerializeField] private BulletFactory _bulletFactory;
    private Queue<Vector3> _spawnPoints;
    private ObservableList<Enemy> _enemies;

    [SerializeField] private Game _gameLogic;
    [SerializeField] private WinConditions _winConditions;
    [SerializeField] private LoseConditions _loseConditions;
    [SerializeField] private float _timeToWin;
    [SerializeField] private int _enemyCountToLose;

    private void Awake()
    {
        _enemies = new ObservableList<Enemy>();
        _spawnPoints = new Queue<Vector3>();

        foreach (var point in _spawnPointsTransform)
        {
            _spawnPoints.Enqueue(point.position);
        }

        _enemyFactory.Initialize();

        _enemySpawner = new EnemySpawner(_enemyFactory, _enemies, this, _spawnPoints);

        _bulletFactory.Initialize();

        GameObject playerPrefab = Instantiate(_playerPrefab, _playerSpawnPoint.position, Quaternion.identity);
        _playerController = playerPrefab.GetComponent<PlayerController>();
        _playerController.Initialize(_bulletFactory);

        _gameLogic.Initialize(_winConditions, _loseConditions, _timeToWin, _enemyCountToLose, _enemies, _enemySpawner, _playerController);
    }
}
