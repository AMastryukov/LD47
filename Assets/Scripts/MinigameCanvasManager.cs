using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameCanvasManager : MonoBehaviour
{
    [SerializeField] private Canvas artMinigameCanvas;
    [SerializeField] private Canvas musicMinigameCanvas;
    [SerializeField] private Canvas codeMinigameCanvas;

    public void OpenArtMinigame()
    {
        CloseAllMinigames();

        artMinigameCanvas.enabled = true;
    }
    
    public void OpenMusicMinigame()
    {
        CloseAllMinigames();
        musicMinigameCanvas.enabled = true;
    }
    
    public void OpenCodeMinigame()
    {
        CloseAllMinigames();
        codeMinigameCanvas.enabled = true;
    }

    public void CloseAllMinigames()
    {
        artMinigameCanvas.enabled = false;
        musicMinigameCanvas.enabled = false;
        codeMinigameCanvas.enabled = false;
    }
}
