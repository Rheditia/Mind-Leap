using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VesselLocomotion : MonoBehaviour
{
    VesselInputHandler inputHandler;
    Entity entity;
    VesselWeapon weapon;
    Rigidbody2D myRigidbody;
    Animator anim;

    [Header("Stats")]
    [SerializeField] float moveVelocity = 5f;
    [SerializeField] float jumpVelocity = 10f;
    [SerializeField] float patrolDuration = 3f;
    [SerializeField] float patrolDelayDuration = 3f;
    private float patrolCounter;
    private float patrolDelayCounter;

    [Header("Checks")]
    [SerializeField] Vector3 groundCheckOffset;
    [SerializeField] Vector2 groundCheckSize;
    [SerializeField] Vector3 ledgeCheckOffset;
    [SerializeField] float ledgeCheckDistance = 0.5f;
    [SerializeField] Vector3 wallCheckOffset;
    [SerializeField] float wallCheckDistance = 0.5f;
    [SerializeField] LayerMask groundMask;

    private bool isPatroling;

    private void Awake()
    {
        inputHandler = GetComponent<VesselInputHandler>();
        entity = GetComponent<Entity>();
        weapon = GetComponent<VesselWeapon>();
        myRigidbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        patrolDelayCounter = patrolDelayDuration;
    }

    private void Update()
    {
        if (!entity.isAbilityDisabled)
        {
            Move();
            Jump();
        }

        PatrolCountdown();
        Patrol();

        FlipSprite();
        SetAnimationParameter();
    }

    private void Move()
    {
        if (weapon.isAttacking && CheckIfGrounded()) 
        {
            myRigidbody.velocity = new Vector2(0, myRigidbody.velocity.y);
            return; 
        }

        Vector2 newVelocity = new Vector2(inputHandler.MoveInput.x * moveVelocity, myRigidbody.velocity.y);
        myRigidbody.velocity = newVelocity;
    }

    private void Patrol()
    {
        if (entity.isAbilityDisabled && !isPatroling) { isPatroling = true; }
        else if (!entity.isAbilityDisabled && isPatroling) { isPatroling = false; }

        if (isPatroling && CheckIfGrounded())
        {
            Vector2 newVelocity = Vector2.zero;            

            if (!CheckForLedge() || CheckForWall())
            {
                transform.localScale = new Vector3(-transform.localScale.x, 1, 1);
            }

            if(patrolDelayCounter > 0)
            {
                newVelocity = new Vector2(0, myRigidbody.velocity.y);
            }
            else if(patrolCounter > 0)
            {
                newVelocity = new Vector2(transform.localScale.x * moveVelocity, myRigidbody.velocity.y);
            }
            myRigidbody.velocity = newVelocity;
        }
    }

    private void PatrolCountdown()
    {
        if (!isPatroling) { return; }
        if (patrolCounter > 0) { patrolCounter -= Time.deltaTime; }
        else if (patrolCounter < 0)
        {
            patrolCounter = 0;
            patrolDelayCounter = patrolDelayDuration; 
        }
        if (patrolDelayCounter > 0) { patrolDelayCounter -= Time.deltaTime; }
        else if (patrolDelayCounter < 0)
        {
            patrolDelayCounter = 0;
            patrolCounter = patrolDuration;
        }
    }

    private void Jump()
    {
        if (weapon != null)
        {
            if (weapon.isAttacking) { return; }
        }
        if (!CheckIfGrounded() || !inputHandler.JumpInput) { return; }
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

    public bool CheckIfGrounded()
    {
        return Physics2D.OverlapBox(transform.position + groundCheckOffset, groundCheckSize, 0, groundMask);
    }

    private bool CheckForLedge()
    {
        Vector3 offset = new Vector3(ledgeCheckOffset.x * transform.localScale.x, ledgeCheckOffset.y, ledgeCheckOffset.z);
        return Physics2D.Raycast(transform.position + offset, Vector2.down, ledgeCheckDistance, groundMask);
    }

    private bool CheckForWall()
    {
        Vector3 offset = new Vector3(wallCheckOffset.x * transform.localScale.x, wallCheckOffset.y, wallCheckOffset.z);
        return Physics2D.Raycast(transform.position + offset, Vector2.right * transform.localScale.x, wallCheckDistance, groundMask);
    }

    private void FlipSprite()
    {
        if (isPatroling) { return; }
        if(Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon)
        {
            transform.localScale = new Vector3(Mathf.Sign(myRigidbody.velocity.x), 1, 1);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(transform.position + groundCheckOffset, groundCheckSize);
        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(transform.position + new Vector3(ledgeCheckOffset.x * transform.localScale.x, ledgeCheckOffset.y, ledgeCheckOffset.z), 
                        transform.position + new Vector3(ledgeCheckOffset.x * transform.localScale.x, ledgeCheckOffset.y, ledgeCheckOffset.z) + Vector3.down * ledgeCheckDistance);
        Gizmos.DrawLine(transform.position + new Vector3(wallCheckOffset.x * transform.localScale.x, wallCheckOffset.y, wallCheckOffset.z), 
                        transform.position + new Vector3(wallCheckOffset.x * transform.localScale.x, wallCheckOffset.y, wallCheckOffset.z) + Vector3.right * transform.localScale.x * wallCheckDistance);
    }
}
