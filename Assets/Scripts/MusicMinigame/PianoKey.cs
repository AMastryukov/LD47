using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PianoKey : MonoBehaviour
{
    public static Action<bool> onKeyPressed;

    [SerializeField] private KeyCode keyCode;

    private AudioSource keySound;

    // Variables for beat overlap
    private Beat overlappingBeat;
    private bool beatIsOverlapping = false;

    private void Awake()
    {
        keySound = GetComponent<AudioSource>();
    }

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
            onKeyPressed?.Invoke(beatIsOverlapping);

            if (beatIsOverlapping)
            {
                keySound.Play();

                Destroy(overlappingBeat.gameObject);
                beatIsOverlapping = false;
            }
        }
    }
}
