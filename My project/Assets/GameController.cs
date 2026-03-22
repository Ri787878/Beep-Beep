using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameController : MonoBehaviour
{
    public static GameController Instance;
    const string GAME_SCENE_NAME = "Game-Scene";
    
    public int numberOfPlayers;
    public int currentPlayer = 0;
    public int[] playerPositions = new int[3];
    public List<string> minigameNames = new List<string>();
    private DiceValueReader dice;
    private static int lastDiceValue;

    void Start()
    {
        DontDestroyOnLoad(gameObject);

        if (Instance == null)
        {
            Instance = this;
            StartGame(1);
        }
        else
        {
            Destroy(this);
        }
        
        dice = GameObject.FindGameObjectWithTag("Dice").gameObject.GetComponent<DiceValueReader>();
    }
    
    public static void StartGame(int numberOfPlayers)
    {
        Instance.numberOfPlayers = numberOfPlayers;
        NextTurn();
    }

    public static void EndGame()
    {
        SceneManager.LoadScene("End"); //TODO
    }
    
    public static void NextTurn()
    {
        Instance.currentPlayer = (Instance.currentPlayer + 1) % Instance.numberOfPlayers;
        
    }
    
    public static void LoadNextMiniGame(int diceValue)
    {
        var randomMiniGameIndex = Random.Range(0, Instance.minigameNames.Count);
        var miniGameName = Instance.minigameNames[randomMiniGameIndex];
        lastDiceValue = diceValue;
        SceneManager.LoadScene(miniGameName);
    }

    public static void EndMiniGame(bool won)
    {
        SceneManager.LoadScene(GAME_SCENE_NAME);


        if (!won)
        {
            NextTurn();
            return;
        }
        
        var player = Instance.currentPlayer + 1;
        
        MovePlayer(lastDiceValue, player);
        
        NextTurn();
    }
    
    public static void MovePlayer(int diceValue, int playerId)
    {
        GameObject player = GameObject.Find("Player" + playerId);
        
        var currentPosition = Instance.playerPositions[playerId - 1];
        var totalPlayerPositions = player.GetComponent<PlayerPositionController>().positions.Count;
        var newPosition = (currentPosition + diceValue) % totalPlayerPositions; // Assuming there are 20 positions on the board
        newPosition++;

        if (newPosition >= totalPlayerPositions)
        {
            EndGame();
        }

        Instance.playerPositions[playerId - 1] = newPosition;
        
        var position = player.GetComponent<PlayerPositionController>().positions[newPosition];
        player.transform.position = position.transform.position;
    }
}
