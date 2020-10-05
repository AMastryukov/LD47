using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CustomButton : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler
{
    private AudioManager audioManager;

    void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }

    public void OnPointerEnter(PointerEventData ped)
    {
        audioManager?.PlayButtonHighlight();
    }

    public void OnPointerDown(PointerEventData ped)
    {
        audioManager?.PlayButtonClick();
    }
}
