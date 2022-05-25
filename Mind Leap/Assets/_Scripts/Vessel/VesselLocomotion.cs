using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VesselLocomotion : MonoBehaviour
{
    VesselInputHandler input;
    Rigidbody2D myRigidbody;
    Animator anim;

    [Header("Stats")]
    [SerializeField] float moveVelocity = 5f;
    [SerializeField] float jumpVelocity = 10f;

    [Header("Checks")]
    [SerializeField] Vector3 groundCheckOffset;
    [SerializeField] Vector2 groundCheckSize;
    [SerializeField] LayerMask groundMask;

    private void Awake()
    {
        input = GetComponent<VesselInputHandler>();
        myRigidbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        Move();
        Jump();
        FlipSprite();
        SetAnimationParameter();
    }

    private void Move()
    {
        Vector2 newVelocity = new Vector2(input.MoveInput.x * moveVelocity, myRigidbody.velocity.y);
        myRigidbody.velocity = newVelocity;
    }

    private void Jump()
    {
        if (!CheckIfGrounded() || !input.JumpInput) { return; }
        Vector2 newVelocity = new Vector2(myRigidbody.velocity.x, jumpVelocity);
        myRigidbody.velocity = newVelocity;
    }

    private void SetAnimationParameter()
    {
        if (Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon && CheckIfGrounded()) { anim.SetBool("move", true); }
        else { anim.SetBool("move", false); }

        if (!CheckIfGrounded()) { anim.SetBool("jump", true); }
        else { anim.SetBool("jump", false); }
    }

    private bool CheckIfGrounded()
    {
        return Physics2D.OverlapBox(transform.position + groundCheckOffset, groundCheckSize, 0, groundMask);
    }

    private void FlipSprite()
    {
        if(Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon)
        {
            transform.localScale = new Vector3(Mathf.Sign(myRigidbody.velocity.x), 1, 1);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(transform.position + groundCheckOffset, groundCheckSize);
    }
}