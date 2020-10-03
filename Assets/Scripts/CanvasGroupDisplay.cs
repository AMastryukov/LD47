using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class CanvasGroupDisplay : MonoBehaviour
{
    private CanvasGroup canvasGroup;
    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OpenDisplay()
    {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
    }

    public void CloseDisplay()
    {
        canvasGroup.alpha = 0f;
        canvasGroup.blocksRaycasts = false;
    }
}
