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
    private Queue<Vector3> _spawnPoints;
    private Vector3 _currentPoint;
    private List<Enemy> _enemies;

    [SerializeField] private WinConditions _winConditions;
    [SerializeField] private LoseConditions _loseConditions;
    private IGameCondition _winCondition;
    private IGameCondition _loseCondition;
    [SerializeField] private float _timeToWin;
    [SerializeField] private int _enemyCountToLose;

    private void Awake()
    {
        _enemies = new List<Enemy>();
        _spawnPoints = new Queue<Vector3>();

        foreach (var point in _spawnPointsTransform)
        {
            _spawnPoints.Enqueue(point.position);
        }

        _currentPoint = _spawnPoints.Dequeue();

        _enemyFactory.Initialize();
        StartCoroutine(SpawnEnemy());

        _bulletFactory.Initialize();

        GameObject playerPrefab = Instantiate(_playerPrefab, _playerSpawnPoint.position, Quaternion.identity);
        _playerController = playerPrefab.GetComponent<PlayerController>();
        _playerController.Initialize(_bulletFactory);

        DetermineWinCondition();
        DetermineLoseCondition();

        Enable();
    }

    private void Update()
    {
        _winCondition.Update();
        _loseCondition.Update();
    }

    public void OnWinConditionChanged()
    {
        Debug.Log("You win");
        Disable();
        StopCoroutine(SpawnEnemy());
    }

    public void OnLoseConditionChanged()
    {
        Debug.Log("You lose");
        Disable();
        StopCoroutine(SpawnEnemy());
    }

    private void Enable()
    {
        _winCondition.ConditionChanged += OnWinConditionChanged;
        _loseCondition.ConditionChanged += OnLoseConditionChanged;
    }

    private void Disable()
    {
        _winCondition.ConditionChanged -= OnWinConditionChanged;
        _loseCondition.ConditionChanged -= OnLoseConditionChanged;
    }

    private IEnumerator SpawnEnemy()
    {
        while (true)
        {
            var enemy = _enemyFactory.Spawn(_currentPoint);
            _enemies.Add(enemy);

            _spawnPoints.Enqueue(_currentPoint);
            _currentPoint = _spawnPoints.Dequeue();

            yield return new WaitForSeconds(1);
        }
    }

    private void DetermineWinCondition()
    {
        switch (_winConditions)
        {
            case WinConditions.TimeToWin:
                _winCondition = new TimeToWinCondition(_timeToWin);
                break;
            case WinConditions.KillEnemies:
                _winCondition = new KillEnemiesCondition(_enemies);
                break;
        }
    }

    private void DetermineLoseCondition()
    {
        switch (_loseConditions)
        {
            case LoseConditions.PlayerDied:
                _loseCondition = new PlayerDiedCondition(_playerController);
                break;
            case LoseConditions.MoreEnemies:
                _loseCondition = new MoreEnemiesCondition(_enemyCountToLose, _enemies);
                break;
        }
    }
}
