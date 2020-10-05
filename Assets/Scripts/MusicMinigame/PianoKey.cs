using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class PianoKey : MonoBehaviour
{
    public static Action<bool> onKeyPressed;

    [SerializeField] private Image noteAreaImage;
    [SerializeField] private Color correctNoteColor;
    [SerializeField] private Color wrongNoteColor;
    [SerializeField] private KeyCode keyCode;

    // Variables for beat overlap
    private Beat overlappingBeat;
    private bool beatIsOverlapping = false;
    private Color defaultNoteAreaColor;

    private void Awake()
    {
        defaultNoteAreaColor = noteAreaImage.color;
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

            StartCoroutine(FlashImageColor(beatIsOverlapping));

            if (beatIsOverlapping)
            {
                Destroy(overlappingBeat.gameObject);
                beatIsOverlapping = false;
            }
        }
    }

    private IEnumerator FlashImageColor(bool success)
    {
        if (success) { noteAreaImage.color = correctNoteColor; }
        else { noteAreaImage.color = wrongNoteColor; }

        yield return new WaitForSeconds(0.25f);

        noteAreaImage.color = defaultNoteAreaColor;
    }
}
