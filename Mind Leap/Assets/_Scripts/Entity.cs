using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    private GameManager gameManager;
    private VesselInputHandler inputHandler;
    private BoxCollider2D myCollider;
    private TimeManager timeManager;
    private GameObject mindEntity;
    private Mind mind;

    [SerializeField] GameObject pointer;
    [SerializeField] float releaseDistance = 2f;

    private int vesselLayer;
    private int playerLayer;
    public bool isAbilityDisabled { get; private set; }

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        inputHandler = GetComponent<VesselInputHandler>();
        myCollider = GetComponent<BoxCollider2D>();
        timeManager = FindObjectOfType<TimeManager>();

        vesselLayer = LayerMask.NameToLayer("Vessel");
        playerLayer = LayerMask.NameToLayer("Player");
    }

    private void Start()
    {
        inputHandler.DisableControl();
        if (mindEntity == null) { isAbilityDisabled = true; }
    }

    private void Update()
    {
        if (myCollider.IsTouchingLayers(LayerMask.GetMask("Hazard"))) 
        { 
            Die(); 
        }
        ReleaseMind();
    }

    private void ReleaseMind()
    {
        Vector3 releaseDirection = new Vector3(inputHandler.MoveInput.x, inputHandler.MoveInput.y, 0).normalized;
        Vector3 releasePosition;
        float releaseAngle;

        if (inputHandler.MoveInput == Vector2.zero || inputHandler.MoveInput.y == -1)
        {
            releasePosition = new Vector3(transform.localScale.x, 0, 0) * releaseDistance;
            releaseAngle = Mathf.Acos(transform.localScale.x) * Mathf.Rad2Deg;
        }
        else
        {
            releasePosition = releaseDirection * releaseDistance;
            releaseAngle = Mathf.Atan2(releaseDirection.y, releaseDirection.x) * Mathf.Rad2Deg;
        }

        if (inputHandler.ReleaseInputPerformed && mindEntity != null)
        {
            timeManager.DoSlowMotion();
            pointer.SetActive(true);
            pointer.transform.localScale = transform.localScale;
            pointer.transform.rotation = Quaternion.Euler(0, 0, releaseAngle);
            isAbilityDisabled = true;
        }
        else if (inputHandler.ReleaseInputCanceled && mindEntity != null)
        {
            timeManager.DoNormalTime();
            pointer.SetActive(false);
            mindEntity.SetActive(true);
            mindEntity.transform.parent = null;

            mindEntity.transform.position = transform.position + releasePosition;
            mindEntity.transform.rotation = Quaternion.Euler(0, 0, releaseAngle);
            mindEntity.transform.localScale = Vector3.one; // when posessing vessel with x scale : -1, the mind scale will change as well

            mind.ResetLifeCounter();
            mind.IsPossessing = false;

            mindEntity = null;
            gameObject.layer = vesselLayer;
            inputHandler.DisableControl();
        }
    }

    public void Die()
    {
        if(mindEntity != null) { mind.Die(); }
        gameManager.DecreaseCount();
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            mindEntity = collision.gameObject;
            if(mindEntity == null) { return; } // prevent double collision

            mind = mindEntity.GetComponent<Mind>();
            if (mind != null) { mind.IsPossessing = true; }

            mindEntity.transform.SetParent(transform);
            mindEntity.transform.rotation = Quaternion.Euler(0, 0, 0);

            mindEntity.SetActive(false);
            gameObject.layer = playerLayer;
            inputHandler.EnableControl();
            isAbilityDisabled = false;
        }
    }
}
