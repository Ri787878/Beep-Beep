using System;
using UnityEngine;

public class Ending : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        try
        {
            var gameEnded = GameController.Instance.hasEnded;
            var winner = GameController.Instance.winnerId;
            if (gameEnded == null || winner == null || gameEnded == false || winner == 0)
            {
                this.gameObject.SetActive(false);
                return;
            }
        
            if (gameEnded)
            {
                if (gameObject.name == "End_Button")
                {
                    gameObject.SetActive(true);
                }
                else if (gameObject.name == ("End_" + winner))
                {
                    gameObject.SetActive(true);    
                } else {
                    gameObject.SetActive(false);
                }
            }
        }
        catch (Exception e)
        {
            this.gameObject.SetActive(false);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
