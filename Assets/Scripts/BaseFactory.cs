using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseFactory<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] private T _objectPrefab;
    [SerializeField] private Transform _container;
    [SerializeField] private bool _autoExpand = true;
    [SerializeField] private int _count;
    private ObjectPool<T> _pool;

    public void Initialize()
    {
        _pool = new ObjectPool<T>(_objectPrefab, _autoExpand, _container, _count);
    }

    public virtual T Spawn(Vector3 spawnPoint)
    {
        return _pool.GetFreeElement(spawnPoint);
    }
}
