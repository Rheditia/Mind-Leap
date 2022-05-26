using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    private VesselInputHandler inputHandler;
    private GameObject mind;

    [SerializeField] float releaseDistance = 2f;

    private int vesselLayer;
    private int playerLayer;

    private void Awake()
    {
        inputHandler = GetComponent<VesselInputHandler>();

        vesselLayer = LayerMask.NameToLayer("Vessel");
        playerLayer = LayerMask.NameToLayer("Player");
    }

    private void Start()
    {
        inputHandler.DisableControl();
    }

    private void Update()
    {
        ReleaseMind();
    }

    private void ReleaseMind()
    {
        if (inputHandler.ReleaseInput && mind != null)
        {
            mind.SetActive(true);
            mind.transform.parent = null;

            Vector3 releaseDirection = new Vector3(inputHandler.MoveInput.x, inputHandler.MoveInput.y, 0).normalized;
            Vector3 releasePosition;
            float releaseAngle;

            if (inputHandler.MoveInput == Vector2.zero) 
            {
                releasePosition = new Vector3(transform.localScale.x, 0, 0) * releaseDistance;
                releaseAngle = Mathf.Acos(transform.localScale.x) * Mathf.Rad2Deg;
            }
            else
            {
                releasePosition = releaseDirection * releaseDistance;
                releaseAngle = Mathf.Atan2(releaseDirection.y, releaseDirection.x) * Mathf.Rad2Deg;
            }

            mind.transform.position = transform.position + releasePosition;
            mind.transform.rotation = Quaternion.Euler(0, 0, releaseAngle);
            mind.transform.localScale = Vector3.one; // when posessing vessel with x scale : -1, the mind scale will change as well

            mind = null;
            gameObject.layer = vesselLayer;
            inputHandler.DisableControl();
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            mind = collision.gameObject;

            mind.transform.SetParent(transform);
            mind.transform.rotation = Quaternion.Euler(0, 0, 0);

            mind.SetActive(false);
            gameObject.layer = playerLayer;
            inputHandler.EnableControl();
        }
    }
}
