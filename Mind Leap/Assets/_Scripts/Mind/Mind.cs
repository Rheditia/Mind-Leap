using UnityEngine;
using UnityEngine.UI;

public class Mind : MonoBehaviour
{
    GameManager gameManager;
    CircleCollider2D myCollider;
    [SerializeField] Image timer;

    [SerializeField] float mindLifeDuration = 3f;
    private float mindLifeCounter;
    private bool isPossessing;
    public bool IsPossessing
    {
        set { isPossessing = value; }
    }

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
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
        if (isPossessing) { return; }
        if (mindLifeCounter > 0) { mindLifeCounter -= Time.deltaTime; }
        else { Die(); }
    }

    public void Die()
    {
        gameManager.ResetLevel();
        Destroy(gameObject);
    }

    public void ResetLifeCounter()
    {
        mindLifeCounter = mindLifeDuration;
    }
}
