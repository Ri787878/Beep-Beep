using System.Collections.Generic;
using UnityEngine;

public class PlayerPositionController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public List<GameObject> positions = new List<GameObject>();
    void Start()
    {
        var id = tag.Substring(6);
        int playerId = int.Parse(id) - 1;
        
        if (GameController.Instance == null)
        {
            var position = positions[0].transform.position;
            transform.position = position;
            return;
        }
        
        var numberOfPlayers = GameController.Instance.numberOfPlayers;
        
        if (int.Parse(id) > numberOfPlayers)
        {
            gameObject.SetActive(false);
        }
        
        int playerPosition = GameController.Instance.playerPositions[playerId];
        
        var postion = positions[playerPosition].transform.position;
        transform.position = postion;
        
        
    }
    // Update is called once per frame
    void Update()
    {
    }
}
