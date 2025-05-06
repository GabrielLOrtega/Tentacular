using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sparks : MonoBehaviour
{
    public AudioSource audioSource;

void OnTriggerEnter(Collider other)
{
	if (other.tag == "player" && !audioSource.isPlaying)
	{
		audioSource.Play();
	}
}
    
}
