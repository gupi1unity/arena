using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionController : MonoBehaviour
{
    private IGameCondition _winCondition;
    private IGameCondition _loseCondition;

    private WinConditions _winConditions;
    private LoseConditions _loseConditions;

    private float _timeToWin;
    private List<Enemy> _enemies;
    private PlayerController _playerController;
    private int _enemyCountToLose;

    public void Initialize(WinConditions winConditions, LoseConditions loseConditions, float timeToWin, List<Enemy> enemies, PlayerController playerController, int enemyCountToLose)
    {
        _winConditions = winConditions;
        _loseConditions = loseConditions;
        _timeToWin = timeToWin;
        _enemies = enemies;
        _playerController = playerController;
        _enemyCountToLose = enemyCountToLose;

        DetermineWinCondition();
        DetermineLoseCondition();
    }

    private void Update()
    {
        _winCondition.Update();
        _loseCondition.Update();
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
