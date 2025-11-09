using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.GridBrushBase;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField]
    private float _moveSpeed = 20f;
    [SerializeField]
    private float _rotationSpeed = 100f;

    private GamePlay _inputActions;

    private Vector3 _movement;
    private float _rotation;

    private void OnEnable()
    {
        _inputActions = new GamePlay();
        if (_inputActions == null)
        {
            Debug.LogWarning("Player Input Action is Null!");
        }
        else
        {
            _inputActions.Movement.Enable();
        }
    }

    private void OnDisable()
    {
        _inputActions.Movement.Disable();
    }

    void Update()
    {
        Vector2 direction = _inputActions.Movement.Move.ReadValue<Vector2>();
        MovePlayer(direction);
    }

    private void MovePlayer(Vector2 direction)
    {
        Vector3 forwardDirection = new Vector3(0, 0, 1);

        // Calculate movement and rotation vectors
        _rotation = _rotationSpeed * Time.deltaTime * direction.x;
        _movement = forwardDirection * _moveSpeed * Time.deltaTime * direction.y;

        // Change directiomn of player
        transform.Rotate(0, _rotation, 0, Space.World);
        // Change direction of player
        transform.Translate(_movement, Space.Self);
    }

    private float ClampAngle(float angle, float min, float max)
    {
        if (angle > 180f)
        {
            angle -= 360f;
        }
        return Mathf.Clamp(angle, min, max);
    }
}
