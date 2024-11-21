using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> where T : MonoBehaviour
{
    private T _prefab;
    private bool _autoExpand;

    private Transform _container;

    private List<T> _pool;

    private MonoBehaviour _context;

    public ObjectPool(T prefab, bool autoExpand, Transform container, int count)
    {
        _prefab = prefab;
        _autoExpand = autoExpand;
        _container = container;

        CreatePool(count);
    }

    private void CreatePool(int count)
    {
        _pool = new List<T>();

        for (int i = 0; i < count; i++)
        {
            CreateObject();
        }
    }

    private T CreateObject(bool isActiveByDefault = false)
    {
        var element = Object.Instantiate(_prefab, _container);
        element.gameObject.SetActive(isActiveByDefault);
        _pool.Add(element);
        return element;
    }

    private bool HasFreeElement(out T element)
    {
        foreach (var poolObject in _pool)
        {
            if (poolObject.gameObject.activeInHierarchy == false)
            {
                element = poolObject;
                element.gameObject.SetActive(true);
                return true;
            }
        }

        element = null;
        return false;
    }

    public T GetFreeElement(Vector3 spawnPoint)
    {
        if (HasFreeElement(out var element))
        {
            element.transform.position = spawnPoint;
            return element;
        }

        if (_autoExpand)
        {
            var poolObject = CreateObject(true);
            poolObject.transform.position = spawnPoint;
            return poolObject;
        }

        throw new System.Exception("No free element");
    }
}
