using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] float resetDelay = 1f;
    [SerializeField] GameObject exitBlock;

    private int vesselCount;

    private void Start()
    {
        vesselCount = FindObjectsOfType<Entity>().Length;
    }

    public void DecreaseCount()
    {
        vesselCount -= 1;
        if (vesselCount <= 0)
        {
            OpenExit();
        }
    }

    private void OpenExit()
    {
        exitBlock.SetActive(false);
    }

    public void ResetLevel()
    {
        StartCoroutine(Reset());
    }

    IEnumerator Reset()
    {
        yield return new WaitForSecondsRealtime(resetDelay);
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
