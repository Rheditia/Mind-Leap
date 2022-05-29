using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mind : MonoBehaviour
{
    [SerializeField] float mindLifeDuration = 3f;
    private float mindLifeCounter;
    private bool isPossessing;
    public bool IsPossessing
    {
        set { isPossessing = value; }
    }

    private void Start()
    {
        mindLifeCounter = mindLifeDuration;
    }

    private void Update()
    {
        LifeCountdown();
    }

    private void LifeCountdown()
    {
        if (isPossessing) { return; }
        if (mindLifeCounter > 0) { mindLifeCounter -= Time.deltaTime; }
        else { Die(); }
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    public void ResetLifeCounter()
    {
        mindLifeCounter = mindLifeDuration;
    }
}
