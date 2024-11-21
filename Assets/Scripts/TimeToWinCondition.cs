using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeToWinCondition : IGameCondition
{
    public event Action ConditionChanged;

    private float _timeToWin;

    public TimeToWinCondition(float timeToWin)
    {
        _timeToWin = timeToWin;
    }

    public void Update()
    {
        _timeToWin -= Time.deltaTime;

        if (_timeToWin < 0)
        {
            ConditionChanged?.Invoke();
        }
    }
}
