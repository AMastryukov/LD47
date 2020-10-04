using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    [SerializeField] private KeyCode keyCode;
    public MusicMinigameManager gameManager;

    // Variables for beat overlap
    private Beat overlappingBeat;
    private bool beatIsOverlapping = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        overlappingBeat = other.GetComponent<Beat>();
        if (overlappingBeat != null) { beatIsOverlapping = true; }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        // Ensure that the collider exiting the key is a Beat
        if (other.GetComponent<Beat>() != null)
        {
            overlappingBeat = null;
            beatIsOverlapping = false;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(keyCode))
        {
            if (beatIsOverlapping)
            {
                Destroy(overlappingBeat.gameObject);
                beatIsOverlapping = false;
                gameManager.AddScore(gameManager.beatHitScore);
            }
            else
            {
                gameManager.AddScore(gameManager.beatMissScore);
            }
        }
    }
}
