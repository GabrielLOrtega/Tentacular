
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Add this to use TextMeshPro

public class ReactorFix : MonoBehaviour
{
    //public ParticleSystem sparks; // Reference to sparking effect
    public Animator animator; // Reference to animation
    public TextMeshProUGUI interactionText; // Reference to TextMeshPro UI element
    public Camera playerCamera; // Reference to the player's camera
    public CollectedItems score;

    private bool isFixed = false;
    public float fixTime = 3f; // Time required to fix the reactor
    private float currentFixTime = 0f; // Tracks fixing progress

    void Update()
    {
        // Check if player is holding 'E' key and looking at the reactor
        if (Input.GetKey(KeyCode.E) && !isFixed && IsLookingAtReactor())
        {
            currentFixTime += Time.deltaTime; // Progressively increase the fix time
            Debug.Log("Reactor Fix Progress: " + (currentFixTime / fixTime) * 100 + "%");

            // When fix time is reached, finalize repair
            if (currentFixTime >= fixTime)
            {
                FixReactor();
            }
        }
        else
        {
            // Reset progress if player stops holding 'E' or looks away
            currentFixTime = 0f;
        }
    }

    bool IsLookingAtReactor()
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

    void FixReactor()
    {
        isFixed = true;
        score.score += 1;
        // Play fixing animation
        // animator.SetTrigger("Fix");

        // Stop sparking effect
        //sparks.Stop();
        //Debug.Log("Reactor Sparks should be stopped now.");

        Debug.Log("Reactor Fully Fixed!");

        // Hide interaction text
        interactionText.text = "";
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isFixed)
        {
            Debug.Log("Press and hold E to fix the reactor.");
            interactionText.text = "Press and hold E to fix the reactor.";
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
