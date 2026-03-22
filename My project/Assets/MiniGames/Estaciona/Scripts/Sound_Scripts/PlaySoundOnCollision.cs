using UnityEngine;

public class PlaySoundOnCollision : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] sounds;

    public void PlayRandomSound()
    {
        if (sounds.Length == 0 || audioSource == null) return;

        int index = Random.Range(0, sounds.Length);
        audioSource.PlayOneShot(sounds[index]);
    }

    private void OnCollisionEnter(Collision collision)
    {
        PlayRandomSound();
    }
}