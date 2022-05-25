using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class VesselInputHandler : MonoBehaviour
{
    PlayerInput input;
    InputAction moveAction;
    InputAction jumpAction;
    InputAction attackAction;

    private void Awake()
    {
        input = GetComponent<PlayerInput>();
        moveAction = input.actions["Move"];
        jumpAction = input.actions["Jump"];
        attackAction = input.actions["Attack"];
    }

    private void OnEnable()
    {
        moveAction.started += OnMoveInput;
        moveAction.performed += OnMoveInput;
        moveAction.canceled += OnMoveInput;

        jumpAction.started += OnJumpInput;
        jumpAction.performed += OnJumpInput;
        jumpAction.canceled += OnJumpInput;

        attackAction.started += OnAttackInput;
        attackAction.performed += OnAttackInput;
        attackAction.canceled += OnAttackInput;
    }

    private void OnDisable()
    {
        moveAction.started -= OnMoveInput;
        moveAction.performed -= OnMoveInput;
        moveAction.canceled -= OnMoveInput;

        jumpAction.started -= OnJumpInput;
        jumpAction.performed -= OnJumpInput;
        jumpAction.canceled -= OnJumpInput;

        attackAction.started -= OnAttackInput;
        attackAction.performed -= OnAttackInput;
        attackAction.canceled -= OnAttackInput;
    }

    private void OnMoveInput(InputAction.CallbackContext context)
    {
        Debug.Log(context.ReadValue<Vector2>());
    }

    private void OnJumpInput(InputAction.CallbackContext context)
    {
        Debug.Log(context.ReadValue<float>());
    }

    private void OnAttackInput(InputAction.CallbackContext context)
    {
        Debug.Log(context.ReadValue<float>());
    }
}
