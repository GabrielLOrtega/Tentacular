
using UnityEngine;

public class RoomVoiceTrigger : MonoBehaviour
{
    public AudioClip voiceLine;
    private AudioSource audioSource;
    private bool hasPlayed = false;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = voiceLine;
        audioSource.playOnAwake = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (!hasPlayed && other.CompareTag("Player"))
        {
            audioSource.Play();
            hasPlayed = true; // Prevent replaying
        }
    }
}
