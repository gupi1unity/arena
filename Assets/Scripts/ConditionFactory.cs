using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionFactory
{
    private WinConditions _winConditions;
    private LoseConditions _loseConditions;

    private IGameCondition _winCondition;
    private IGameCondition _loseCondition;

    private float _timeToWin;
    private int _enemyCountToLose;

    private ObservableList<Enemy> _enemies;
    private PlayerController _playerController;

    public ConditionFactory(WinConditions winConditions, LoseConditions loseConditions, float timeToWin, int enemyCountToLose, ObservableList<Enemy> enemies, PlayerController playerController)
    {
        _winConditions = winConditions;
        _loseConditions = loseConditions;
        _timeToWin = timeToWin;
        _enemyCountToLose = enemyCountToLose;
        _enemies = enemies;
        _playerController = playerController;

        DetermineWinCondition();
        DetermineLoseCondition();
    }

    public IGameCondition GetWinCondition()
    {
        return _winCondition;
    }

    public IGameCondition GetLoseCondition() 
    { 
        return _loseCondition;
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
