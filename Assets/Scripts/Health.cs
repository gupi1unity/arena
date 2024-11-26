using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health
{
    private int _health;

    public int HealthValue { get => _health; }

    public Health(int health)
    {
        _health = health;
    }

    public void AddHealth(int value)
    {
        if (value < 0)
        {
            return;
        }

        _health += value;
    }

    public void RemoveHealth(int value)
    {
        if (value < 0)
        {
            return;
        }

        _health -= value;
    }

    public void UpdateHealth(int health)
    {
        if (health < 0)
        {
            return;
        }

        _health = health;
    }
}
