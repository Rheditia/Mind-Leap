using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VesselLocomotion : MonoBehaviour
{
    VesselInputHandler input;
    Rigidbody2D myRigidbody;
    Animator anim;

    [SerializeField] float moveVelocity = 5f;

    private void Awake()
    {
        input = GetComponent<VesselInputHandler>();
        myRigidbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        Move();
        FlipSprite();
    }

    private void Move()
    {
        Vector2 newVelocity = new Vector2(input.MoveInput.x * moveVelocity, myRigidbody.velocity.y);
        myRigidbody.velocity = newVelocity;

        if(Mathf.Abs(newVelocity.x) > Mathf.Epsilon) { anim.SetBool("move", true); }
        else { anim.SetBool("move", false); }
    }

    private void FlipSprite()
    {
        if(Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon)
        {
            transform.localScale = new Vector3(Mathf.Sign(myRigidbody.velocity.x), 1, 1);
        }
    }
}
