using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float timeLimit = 60f; // Time limit in seconds
    private float currentTime = 0f;
    public bool countDown = true; // If true, timer counts down from timeLimit to 0. If false, counts up from 0 to timeLimit.
    private TextMeshProUGUI timerText;
    public string onEndAction;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timerText = GetComponent<TextMeshProUGUI>();
        
        if (countDown)
            currentTime = timeLimit;
        
        else
            currentTime = 0f;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (countDown)
            currentTime -= Time.deltaTime;
        else
            currentTime += Time.deltaTime;

        timerText.text = Mathf.Ceil(currentTime).ToString();

        if ((countDown && currentTime <= 0) || (!countDown && currentTime >= timeLimit))
        {
            MinigameManager.EndGame(onEndAction);
        }
    }
}
