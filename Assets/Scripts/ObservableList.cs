using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObservableList<T>
{
    public event Action<T> Added;
    public event Action<T> Removed;

    private List<T> _list;

    public List<T> List => _list;

    public ObservableList()
    {
        _list = new List<T>();
    }

    public void Add(T element)
    {
        _list.Add(element);
        Added?.Invoke(element);
    }

    public void Remove(T element)
    {
        _list.Remove(element);
        Removed?.Invoke(element);
    }
}
