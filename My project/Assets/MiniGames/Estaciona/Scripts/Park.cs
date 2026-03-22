using UnityEngine;

public class ParkingZone : MonoBehaviour
{
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            GameController.EndMiniGame(true);

    }
}