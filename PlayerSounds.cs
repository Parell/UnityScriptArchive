using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void PlayerFootstepSound()
    {
        audioSource.Play();
    }
}
