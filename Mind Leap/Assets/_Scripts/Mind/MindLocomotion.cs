using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MindLocomotion : MonoBehaviour
{
    MindInputHandler inputHandler;
    Rigidbody2D myRigidbody;

    [SerializeField] float moveVelocity = 3f;
    [SerializeField] float torqueAmount = 30f;

    private void Awake()
    {
        inputHandler = GetComponent<MindInputHandler>();
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Rotate();
        Move();
    }

    private void Move()
    {
        myRigidbody.velocity = transform.right * moveVelocity;
    }

    private void Rotate()
    {
        if(inputHandler.MoveInput == -1)
        {
            myRigidbody.AddTorque(torqueAmount, ForceMode2D.Force);
        }
        else if (inputHandler.MoveInput == 1)
        {
            myRigidbody.AddTorque(-torqueAmount, ForceMode2D.Force);
        }
    }
}
