using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    private WinConditions _winConditions;
    private LoseConditions _loseConditions;

    private IGameCondition _winCondition;
    private IGameCondition _loseCondition;

    private float _timeToWin;
    private int _enemyCountToLose;

    private ObservableList<Enemy> _enemies;
    private EnemySpawner _enemySpawner;
    private PlayerController _playerController;

    public void Initialize(WinConditions winConditions, LoseConditions loseConditions, float timeToWin, int enemyCountToLose, ObservableList<Enemy> enemies, EnemySpawner enemySpawner, PlayerController playerController)
    {
        _winConditions = winConditions;
        _loseConditions = loseConditions;
        _timeToWin = timeToWin;
        _enemyCountToLose = enemyCountToLose;
        _enemies = enemies;
        _enemySpawner = enemySpawner;
        _playerController = playerController;

        _enemySpawner.StartSpawner();

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
        _enemySpawner.StopSpawner();
    }

    public void OnLoseConditionChanged()
    {
        Debug.Log("You lose");
        Disable();
        _enemySpawner.StopSpawner();
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
