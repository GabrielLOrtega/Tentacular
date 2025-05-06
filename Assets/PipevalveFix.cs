
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Add this to use TextMeshPro

public class PipevalveFix : MonoBehaviour
{
    public List<ParticleSystem> sparksList; // Reference to a list of sparking effects
    public Animator animator; // Reference to animation
    public TextMeshProUGUI interactionText; // Reference to TextMeshPro UI element
    public Camera playerCamera; // Reference to the player's camera
    public CollectedItems score;

    private bool isFixed = false;
    public float fixTime = 3f; // Time required to fix the pipe valve
    private float currentFixTime = 0f; // Tracks fixing progress

    void Update()
    {
        // Check if player is holding 'E' key and looking at the pipe valve
        if (Input.GetKey(KeyCode.E) && !isFixed && IsLookingAtPipeValve())
        {
            currentFixTime += Time.deltaTime; // Progressively increase the fix time
            Debug.Log("Pipe Valve Fix Progress: " + (currentFixTime / fixTime) * 100 + "%");

            // When fix time is reached, finalize repair
            if (currentFixTime >= fixTime)
            {
                FixPipeValve();
            }
        }
        else
        {
            // Reset progress if player stops holding 'E' or looks away
            currentFixTime = 0f;
        }
    }

    bool IsLookingAtPipeValve()
    {
        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject == gameObject)
            {
                return true;
            }
        }
        return false;
    }

    void FixPipeValve()
    {
        isFixed = true;
        score.score += 1;
         
        // Play fixing animation
        // animator.SetTrigger("Fix");

        // Stop all sparking effects
        foreach (ParticleSystem sparks in sparksList)
        {
            sparks.Stop();
        }
        Debug.Log("Pipe Valve Sparks should be stopped now.");

        Debug.Log("Pipe Valve Fully Fixed!");

        // Hide interaction text
        interactionText.text = "";
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isFixed)
        {
            Debug.Log("Press and hold E to fix the pipe valve.");
            interactionText.text = "Press and hold E to fix the pipe valve.";
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            interactionText.text = "";
        }
    }
}
