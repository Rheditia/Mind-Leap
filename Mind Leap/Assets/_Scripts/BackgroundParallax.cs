using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundParallax : MonoBehaviour
{
    [SerializeField] Camera mainCamera;
    [SerializeField] float parallaxFactor;

    private float startPosition;
    private float spriteLength;

    private void Start()
    {
        startPosition = transform.position.x;
        spriteLength = GetComponentInChildren<SpriteRenderer>().bounds.size.x;
    }

    private void Update()
    {
        float spritePositionRelativeToCamera = mainCamera.transform.position.x * (1 - parallaxFactor);
        float distanceMoved = mainCamera.transform.position.x * parallaxFactor;

        // start position + (distance moved * parallax factor)
        transform.position = new Vector3(startPosition + distanceMoved, transform.position.y, transform.position.z);

        if (spritePositionRelativeToCamera > startPosition + spriteLength)
        {
            startPosition += spriteLength;
        }
        else if (spritePositionRelativeToCamera < startPosition - spriteLength)
        {
            startPosition -= spriteLength;
        }
    }
}
