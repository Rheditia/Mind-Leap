using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VesselWeapon : MonoBehaviour
{
    VesselInputHandler input;
    Animator anim;

    [SerializeField] Vector3 attackOffset;
    [SerializeField] float attackRadius;
    [SerializeField] LayerMask enemyMask;

    [SerializeField] float attackDelayDuration = 1f;
    private float attackDelayCountdown;
    private bool attackDelay => attackDelayCountdown <= 0;

    private void Awake()
    {
        input = GetComponent<VesselInputHandler>();
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        // TODO: Move to the method that handle possession
        gameObject.layer = LayerMask.NameToLayer("Player");
    }

    private void Update()
    {
        if(attackDelayCountdown > 0) { attackDelayCountdown -= Time.deltaTime; }
        if (input.AttackInput && attackDelay)
        {
            anim.SetBool("attack", true);
        }
    }

    private void Attack()
    {
        Vector3 offset = new Vector3(attackOffset.x * transform.localScale.x, attackOffset.y, attackOffset.z);
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position + offset, attackRadius, enemyMask);
        // TODO: Deal damage to all enemies
    }

    private void ResetParameter()
    {
        attackDelayCountdown = attackDelayDuration;
        anim.SetBool("attack", false);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + new Vector3(attackOffset.x * transform.localScale.x, attackOffset.y, attackOffset.z), attackRadius);
    }
}
