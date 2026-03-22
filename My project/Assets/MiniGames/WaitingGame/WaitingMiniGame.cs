using UnityEngine;
using UnityEngine.Audio;

public class WaitingMiniGame : MonoBehaviour
{
    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip waitingClip;

    private float timer;
    private bool lost;
    private bool ended;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timer = (float)10.0;
        lost = false;
        ended = false;

        if (audioSource != null && waitingClip != null)
        {
            audioSource.clip = waitingClip;
            audioSource.loop = false;   // clip is 10s, no need to loop
            audioSource.Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (ended) return;

        if (timer > 0)
        {
            if (Input.GetKeyDown(KeyCode.Space))
                lost = true;
            if (Input.GetKeyDown(KeyCode.W))
                lost = true;
            if (Input.GetKeyDown(KeyCode.A))
                lost = true;
            if (Input.GetKeyDown(KeyCode.S))
                lost = true;
            if (Input.GetKeyDown(KeyCode.D))
                lost = true;
            timer -= Time.deltaTime;
        }
        if (lost)
            MinigameManager.EndGame("Lose");
        if (timer < 0)
            MinigameManager.EndGame("Win");
    }
    void Reset()
    {
        // Auto-add/fill AudioSource when the script is added in-editor (optional)
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null) audioSource = gameObject.AddComponent<AudioSource>();
    }

    private void End(string result)
    {
        ended = true;

        // Stop audio when game ends (optional)
        if (audioSource != null && audioSource.isPlaying)
            audioSource.Stop();

        MinigameManager.EndGame(result);
    }

}
