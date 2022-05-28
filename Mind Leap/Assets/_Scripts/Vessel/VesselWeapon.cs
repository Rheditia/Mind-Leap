using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VesselWeapon : MonoBehaviour
{
    VesselInputHandler inputHandler;
    Entity entity;
    VesselLocomotion locomotion;
    Animator anim;

    [Header("Checks")]
    [SerializeField] Vector3 attackOffset;
    [SerializeField] float attackRadius;
    [SerializeField] LayerMask enemyMask;

    [Header("Stats")]
    [SerializeField] float attackDelayDuration = 1f;
    private float attackDelayCountdown;
    private bool attackDelay => attackDelayCountdown <= 0;
    public bool isAttacking { get; private set; }

    private void Awake()
    {
        inputHandler = GetComponent<VesselInputHandler>();
        entity = GetComponent<Entity>();
        locomotion = GetComponent<VesselLocomotion>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if(attackDelayCountdown > 0) { attackDelayCountdown -= Time.deltaTime; }
        if (inputHandler.AttackInput && attackDelay && !entity.isAbilityDisabled && locomotion.CheckIfGrounded())
        {
            anim.SetBool("attack", true);
            isAttacking = true;
        }
    }

    private void Attack()
    {
        Vector3 offset = new Vector3(attackOffset.x * transform.localScale.x, attackOffset.y, attackOffset.z);
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position + offset, attackRadius, enemyMask);
        foreach(Collider2D enemy in enemies)
        {
            enemy.GetComponent<Entity>().Die();
        }
    }

    private void ResetParameter()
    {
        attackDelayCountdown = attackDelayDuration;
        anim.SetBool("attack", false);
        isAttacking = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + new Vector3(attackOffset.x * transform.localScale.x, attackOffset.y, attackOffset.z), attackRadius);
    }
}
