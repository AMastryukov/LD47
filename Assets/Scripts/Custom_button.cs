using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Custom_button : MonoBehaviour, IPointerEnterHandler , IPointerDownHandler
{
    public AudioSource main;
    public AudioClip highlight;
    public AudioClip click;
    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<Image>().alphaHitTestMinimumThreshold = 0.1f;
    }

    public void OnPointerEnter(PointerEventData ped)
    {
        Debug.Log("yes");
        main.PlayOneShot(highlight);
    }

    public void OnPointerDown(PointerEventData ped)
    {
        main.PlayOneShot(click);
    }

}
