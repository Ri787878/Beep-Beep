    using UnityEngine;
using UnityEngine.SceneManagement;

public class Main_Menu_Controller : MonoBehaviour
{
    [Header("Scene to load when clicking Play")]
    [SerializeField] private string gameSceneName = "players";

    public void Play()
    {
        // Load the game scene when "Play" button is pressed
        SceneManager.LoadScene(gameSceneName);
    }

    public void Quit()
    {
        // Quit the application when "Quit" button is pressed
        Application.Quit();
    }
}