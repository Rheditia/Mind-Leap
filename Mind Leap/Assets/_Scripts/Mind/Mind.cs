using UnityEngine;
using UnityEngine.UI;

public class Mind : MonoBehaviour
{
    GameManager gameManager;
    AudioPlayer audioPlayer;
    CircleCollider2D myCollider;
    [SerializeField] Image timer;

    [SerializeField] float mindLifeDuration = 3f;
    private float mindLifeCounter;
    private bool isPossessing;
    public bool IsPossessing
    {
        set { isPossessing = value; }
    }
    private bool isExiting;
    public bool IsExiting
    {
        set { isExiting = value; }
    }

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        audioPlayer = FindObjectOfType<AudioPlayer>();
        myCollider = GetComponent<CircleCollider2D>();
    }

    private void Start()
    {
        mindLifeCounter = mindLifeDuration;
    }

    private void Update()
    {
        if (myCollider.IsTouchingLayers(LayerMask.GetMask("Hazard", "Platform"))) { Die(); }
        LifeCountdown();
        timer.fillAmount = mindLifeCounter / mindLifeDuration;
    }

    private void LifeCountdown()
    {
        if (isPossessing || isExiting) { return; }
        if (mindLifeCounter > 0) { mindLifeCounter -= Time.deltaTime; }
        else { Die(); }
    }

    public void Die()
    {
        if (isExiting) { return; }
        audioPlayer.PlayVanishClip();
        gameManager.ResetLevel();
        Destroy(gameObject);
    }

    public void ResetLifeCounter()
    {
        mindLifeCounter = mindLifeDuration;
    }
}
