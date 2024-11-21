using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDiedCondition : IGameCondition
{
    public event Action ConditionChanged;

    private PlayerController _playerController;

    private bool _isPlayerDied = false;

    public PlayerDiedCondition(PlayerController playerController)
    {
        _playerController = playerController;

        _playerController.PlayerDied += OnPlayerDied;
    }

    public void OnPlayerDied()
    {
        _isPlayerDied = true;
    }

    public void Update()
    {
        if (_isPlayerDied == true)
        {
            ConditionChanged?.Invoke();
        }
    }
}
