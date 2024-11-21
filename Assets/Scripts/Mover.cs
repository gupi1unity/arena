using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover
{
    private const string HorizontalAxisName = "Horizontal";
    private const string VerticalAxisName = "Vertical";

    private float _speed;
    private CharacterController _characterController;

    public Vector3 MoveDirection { get => CalculateMoveDirection(); }

    public Mover(float speed, CharacterController characterController)
    {
        _speed = speed;
        _characterController = characterController;
    }

    private Vector3 CalculateMoveDirection()
    {
        float xInput = Input.GetAxisRaw(HorizontalAxisName);
        float yInput = Input.GetAxisRaw(VerticalAxisName);

        Vector3 _moveDirection = new Vector3(xInput, 0, yInput).normalized;

        return _moveDirection;
    }

    public void Move()
    {
        _characterController.Move(CalculateMoveDirection() * _speed * Time.deltaTime);
    }
}
