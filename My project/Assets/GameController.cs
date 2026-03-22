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
    public bool hasEnded = false;
    public int winnerId = 0;
    private DiceValueReader dice;
    private static int lastDiceValue;
    
    
    void Start()
    {
        DontDestroyOnLoad(gameObject);

        if (Instance == null)
        {
            Instance = this;
            StartGame();
        }
        else
        {
            Destroy(this);
        }

        playerPositions[0] = 0;
        playerPositions[1] = 0;
        playerPositions[2] = 0;
        
        dice = GameObject.FindGameObjectWithTag("Dice").gameObject.GetComponent<DiceValueReader>();
    }
    
    public static void StartGame()
    {
        var numberOfPlayers = PlayerPrefs.GetInt("NumPlayers", 1);
        
        Instance.numberOfPlayers = numberOfPlayers;
        
        for (int i = 3; i > numberOfPlayers; i--)
        {
            var player = GameObject.FindGameObjectWithTag("Player" + i);
            player.SetActive(false); 
        }
        
        NextTurn();
    }

    public static void EndGame(int winnerPlayerId)
    {
        Instance.hasEnded = true;
        Instance.winnerId = winnerPlayerId;
    }

    public static void GoBackToMainMenu()
    {
        ResetController();
        SceneManager.LoadScene("MainMenu");
    }

    private static void ResetController()
    {
        Instance.numberOfPlayers = 0;
        Instance.currentPlayer = 0;
        Instance.playerPositions = new int[3];
        lastDiceValue = 0;
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
/*
    void OnSceneLoaded()
    {
        if (canMovePlayer)
        {
            var player = Instance.currentPlayer + 1;
            MovePlayer(lastDiceValue, player);
        }

        if (canChangeTurn)
        {
            NextTurn();
        }
    }
    */
    public static void MovePlayer(int diceValue, int playerId)
    {
        var currentPosition = Instance.playerPositions[playerId - 1];
        var newPosition = currentPosition + diceValue; // Assuming there are 20 positions on the board

        if (newPosition >= 20)
        {
            EndGame(playerId);
        }

        Instance.playerPositions[playerId - 1] = newPosition;
    }
}
