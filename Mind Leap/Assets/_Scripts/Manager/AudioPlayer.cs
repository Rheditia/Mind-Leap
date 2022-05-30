using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [Header("Attack")]
    [SerializeField] AudioClip attackClip;
    [SerializeField] [Range(0f, 1f)] float attackVolume = 1f;

    [Header("Die")]
    [SerializeField] AudioClip dieClip;
    [SerializeField] [Range(0f, 1f)] float dieVolume = 1f;

    [Header("Vanish")]
    [SerializeField] AudioClip vanishClip;
    [SerializeField] [Range(0f, 1f)] float vanishVolume = 1f;

    static AudioPlayer Instance;

    private void Awake()
    {
        ManageSingleton();
    }

    private void ManageSingleton()
    {
        if (Instance != null)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void PlayAttackClip()
    {
        PlayClip(attackClip, attackVolume);
    }

    public void PlayDieClip()
    {
        PlayClip(dieClip, dieVolume);
    }

    public void PlayVanishClip()
    {
        PlayClip(vanishClip, vanishVolume);
    }

    private void PlayClip(AudioClip clip, float volume)
    {
        if (clip)
        {
            Vector3 cameraPos = Camera.main.transform.position;
            AudioSource.PlayClipAtPoint(clip, cameraPos, volume);
        }
    }

    public void ResetAudio()
    {
        Instance = null;
        Destroy(gameObject);
    }
}
