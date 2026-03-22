using UnityEngine;

public class CrossingAction : MonoBehaviour
{
    public GameObject crossingTile;
    public float timeToStop = 5f;
    private bool crossing = false;
    private AutoScroll autoScroll;

    private float timeToCrossing;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        autoScroll = GetComponent<AutoScroll>();
        timeToCrossing = Random.Range(2, 8f);
    }

    // Update is called once per frame
    void Update()
    {
        if (timeToCrossing > 0)
        {
            timeToCrossing -= Time.deltaTime;
        }
        else if (!crossing)
        {
            autoScroll.RequestNextTile(crossingTile);
            crossing = true;
        }
        
        if (crossing) 
        {
            if (timeToStop > 0)
            {
                timeToStop -= Time.deltaTime;
            }
            else
            {
                autoScroll.scrollSpeed = 0;
                GameController.EndMiniGame(false);
            }
            
            if (Input.GetKeyDown(KeyCode.Space))
            {
                autoScroll.scrollSpeed = 0;
                GameController.EndMiniGame(true);
            }
        }
    }
}
