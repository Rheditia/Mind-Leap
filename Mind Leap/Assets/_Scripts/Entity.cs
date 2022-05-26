using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    private int vesselLayer;
    private int playerLayer;

    private void Awake()
    {
        vesselLayer = LayerMask.NameToLayer("Vessel");
        playerLayer = LayerMask.NameToLayer("Player");
    }

    private void Update()
    {
        
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
