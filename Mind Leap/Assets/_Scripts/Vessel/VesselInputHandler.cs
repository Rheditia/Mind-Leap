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

    public Vector2 MoveInput { get; private set; }
    public bool JumpInput { get; private set; }

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
        MoveInput = context.ReadValue<Vector2>();
    }

    private void OnJumpInput(InputAction.CallbackContext context)
    {
        JumpInput = context.ReadValue<float>() == 1;
    }

    private void OnAttackInput(InputAction.CallbackContext context)
    {
    }
}
