using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator
{
    private Transform _playerTransform;

    public Rotator(Transform playerTransform)
    {
        _playerTransform = playerTransform;
    }

    public void Rotate(Vector3 target)
    {
        _playerTransform.rotation = Quaternion.LookRotation(new Vector3(target.x, 0, target.z), Vector3.up);
    }
}
