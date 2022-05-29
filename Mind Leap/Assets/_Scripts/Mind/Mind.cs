using UnityEngine;
using UnityEngine.UI;

public class Mind : MonoBehaviour
{
    [SerializeField] Image timer;

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
        Destroy(gameObject);
    }

    public void ResetLifeCounter()
    {
        mindLifeCounter = mindLifeDuration;
    }
}
