using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameResultDisplay : MonoBehaviour
{
    [SerializeField] private CanvasGroupDisplay completedGameCGD;
    [SerializeField] private CanvasGroupDisplay unfinishedGameCGD;

    private CanvasGroupDisplay canvasGroupDisplay;

    private void Awake()
    {
        canvasGroupDisplay = GetComponent<CanvasGroupDisplay>();
    }

    public void ShowGameResult(bool completed)
    {
        canvasGroupDisplay.OpenDisplay();

        if (completed)
        {
            completedGameCGD.OpenDisplay();
            unfinishedGameCGD.CloseDisplay();
        }
        else
        {
            completedGameCGD.CloseDisplay();
            unfinishedGameCGD.OpenDisplay();
        }
    }
}
