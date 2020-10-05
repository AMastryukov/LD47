using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameManager : MonoBehaviour
{
    [SerializeField] private Canvas artMinigameCanvas;
    [SerializeField] private Canvas musicMinigameCanvas;
    [SerializeField] private Canvas codeMinigameCanvas;

    [SerializeField] private CodeMinigame codeMinigame;
    [SerializeField] private MusicMinigame musicMinigame;

    public void OpenArtMinigame()
    {
        CloseAllMinigames();

        artMinigameCanvas.enabled = true;
    }
    
    public void OpenMusicMinigame()
    {
        CloseAllMinigames();

        musicMinigameCanvas.enabled = true;
        musicMinigame.ActivateGame(true);
    }
    
    public void OpenCodeMinigame()
    {
        CloseAllMinigames();

        codeMinigameCanvas.enabled = true;
        codeMinigame.ActivateGame(true);
    }

    public void CloseAllMinigames()
    {
        artMinigameCanvas.enabled = false;
        musicMinigameCanvas.enabled = false;
        codeMinigameCanvas.enabled = false;

        codeMinigame.ActivateGame(false);
        musicMinigame.ActivateGame(false);
    }
}
