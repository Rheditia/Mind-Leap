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
    InputAction releaseAction;

    public Vector2 MoveInput { get; private set; }
    public bool JumpInput { get; private set; }
    public bool AttackInput { get; private set; }
    public bool ReleaseInput { get; private set; }

    private void Awake()
    {
        input = GetComponent<PlayerInput>();
        moveAction = input.actions["Move"];
        jumpAction = input.actions["Jump"];
        attackAction = input.actions["Attack"];
        releaseAction = input.actions["Release"];
    }

    public void EnableControl()
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

        releaseAction.started += OnReleaseInput;
        releaseAction.performed += OnReleaseInput;
        releaseAction.canceled += OnReleaseInput;
    }

    public void DisableControl()
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

        releaseAction.started -= OnReleaseInput;
        releaseAction.performed += OnReleaseInput;
        releaseAction.canceled += OnReleaseInput;

        MoveInput = Vector2.zero;
        JumpInput = false;
        AttackInput = false;
        ReleaseInput = false;
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
        AttackInput = context.ReadValue<float>() == 1;
    }

    private void OnReleaseInput(InputAction.CallbackContext context)
    {
        ReleaseInput = context.ReadValue<float>() == 1;
    }
}
