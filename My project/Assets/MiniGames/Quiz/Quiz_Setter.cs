using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI;

public class Quiz_Setter : MonoBehaviour
{
    [Header("Image Component")]
    [SerializeField] public Image targetImage;

    [Header("Image")]
    [SerializeField] public Sprite Image1;
    [SerializeField] public Sprite Image2;
    [SerializeField] public Sprite Image3;

    [Header("Options")]
    [SerializeField] private TMP_Text Option_1;
    [SerializeField] private TMP_Text Option_2;
    [SerializeField] private TMP_Text Option_3;
    [SerializeField] private TMP_Text Option_4;

    private int question;
    public AudioSource audioSource;
    public AudioClip[] sounds;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        question = Random.Range(1, 4);
        PlayRandomSound();
        Set_Options();
        if (question == 1)
            Set_Image(targetImage, Image1);
        else if (question == 2)
            Set_Image(targetImage, Image2);
        else if (question == 3)
            Set_Image(targetImage, Image3);
    }

    // Update is called once per frame
    void Update()
    {
        Read_Input(question);
    }
    public void Win()
    {
        StopSound();
        GameController.EndMiniGame(true);
    }

    public void Lose()
    {
        StopSound();
        GameController.EndMiniGame(false);
    }

    public void StopSound()
    {
        if (audioSource != null)
        {
            audioSource.Stop();
        }
    }

    public void Read_Input(int scenario)
    {
        if (scenario == 1)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
                Lose();
            if (Input.GetKeyDown(KeyCode.Alpha2))
                Lose();
            if (Input.GetKeyDown(KeyCode.Alpha3))
                Win();
            if (Input.GetKeyDown(KeyCode.Alpha4))
                Lose();
        }
        else if (scenario == 2)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
                Lose();
            if (Input.GetKeyDown(KeyCode.Alpha2))
                Win();
            if (Input.GetKeyDown(KeyCode.Alpha3))
                Lose();
            if (Input.GetKeyDown(KeyCode.Alpha4))
                Lose();
        }
        if (scenario == 3)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
                Lose();
            if (Input.GetKeyDown(KeyCode.Alpha2))
                Lose();
            if (Input.GetKeyDown(KeyCode.Alpha3))
                Lose();
            if (Input.GetKeyDown(KeyCode.Alpha4))
                Win();
        }
    }
    public void Set_Image(Image targetImage, Sprite newImage)
    {
        if (targetImage != null && newImage != null)
        {
            targetImage.sprite = newImage;
        }
    }

    public void Set_Options()
    {
        Option_1.text = "1. Paragem de transito da Reta Guarda";
        Option_2.text = "2. Paragem de transito da Frente";
        Option_3.text = "3. Paragem de transito da Frente e da Reta Guarda";
        Option_4.text = "4. Olá amigos condutores :)";
    }

    public void PlayRandomSound()
    {
        if (sounds == null || sounds.Length == 0 || audioSource == null) return;

        int index = Random.Range(0, sounds.Length);
        audioSource.clip = sounds[index];
        audioSource.loop = true;
        audioSource.Play();
    }
}
