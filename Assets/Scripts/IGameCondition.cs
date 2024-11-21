using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGameCondition
{
    public event Action ConditionChanged;

    public void Update();
}
