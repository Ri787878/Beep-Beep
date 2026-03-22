using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Choose_Players_Contoller : MonoBehaviour
{
    [Header("Scene to load when number of players is chosen")]
    [SerializeField] private string boardGame = "Game";

    public Button one_PlayerButton;
    public Button two_PlayersButton;
    public Button three_PlayersButton;

    public int numberOfPlayers;

    public void Start()
    {
        if (one_PlayerButton != null)
            one_PlayerButton.onClick.AddListener(() => SetNumberOfPlayers(1));

        if (two_PlayersButton != null)
            two_PlayersButton.onClick.AddListener(() => SetNumberOfPlayers(2));

        if (three_PlayersButton != null)
            three_PlayersButton.onClick.AddListener(() => SetNumberOfPlayers(3));
    }
    public void SetNumberOfPlayers(int players)
    {
        Debug.Log("CLICKED " + players);

        numberOfPlayers = players;
        PlayerPrefs.SetInt("NumPlayers", numberOfPlayers);
        SceneManager.LoadScene(boardGame);
    }

}
