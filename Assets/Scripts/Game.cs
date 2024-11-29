using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    private IGameCondition _winCondition;
    private IGameCondition _loseCondition;

    private ConditionFactory _conditionFactory;

    private float _timeToWin;
    private int _enemyCountToLose;

    private ObservableList<Enemy> _enemies;
    private EnemySpawner _enemySpawner;
    private PlayerController _playerController;

    public void Initialize(ConditionFactory conditionFactory, EnemySpawner enemySpawner)
    {
        _conditionFactory = conditionFactory;
        _enemySpawner = enemySpawner;

        _enemySpawner.StartSpawner();

        _winCondition = _conditionFactory.GetWinCondition();
        _loseCondition = _conditionFactory.GetLoseCondition();

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
}
