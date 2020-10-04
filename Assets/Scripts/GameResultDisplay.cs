using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameResultDisplay : MonoBehaviour
{
    [SerializeField] private CanvasGroupDisplay completedGameCGD;
    [SerializeField] private CanvasGroupDisplay unfinishedGameCGD;
    [SerializeField] private TextMeshProUGUI nextJamCountdownText;
    [SerializeField] private Button nextJamButton;

    [Header("Finished Game References")]
    [SerializeField] private TextMeshProUGUI gameNameText;
    [SerializeField] private TextMeshProUGUI overallScoreText;
    [SerializeField] private TextMeshProUGUI funScoreText;
    [SerializeField] private TextMeshProUGUI graphicsScoreText;
    [SerializeField] private TextMeshProUGUI audioScoreText;

    private CanvasGroupDisplay canvasGroupDisplay;

    private void Awake()
    {
        canvasGroupDisplay = GetComponent<CanvasGroupDisplay>();
    }

    public void ShowGameResult(bool completed)
    {
        canvasGroupDisplay.OpenDisplay();

        if (completed) { DisplayCompletedGameScreen(); }
        else { DisplayUnfinishedGameScreen();  }

        StartCoroutine(NextJamCountdown());
    }

    private void DisplayCompletedGameScreen()
    {
        completedGameCGD.OpenDisplay();
        unfinishedGameCGD.CloseDisplay();

        GameManager gm = FindObjectOfType<GameManager>();

        gameNameText.text = gm.CurrentGame.Name;

        // TODO: change this to a proper format and calculate it correctly
        overallScoreText.text = "Overall: \t" + "254th";
        funScoreText.text = "Fun: \t\t" + "166th";
        graphicsScoreText.text = "Graphics: \t" + "243rd";
        audioScoreText.text = "Audio: \t" + "92nd";

        // TODO: display the images stored in the game
    }

    private void DisplayUnfinishedGameScreen()
    {
        completedGameCGD.CloseDisplay();
        unfinishedGameCGD.OpenDisplay();
    }

    private IEnumerator NextJamCountdown()
    {
        nextJamButton.interactable = false;

        for (int i = 9; i >= 0; i--)
        {
            yield return new WaitForSeconds(1f);

            string startTime = "in " + i.ToString() + " seconds";

            if (i == 0) { startTime = "NOW"; }

            nextJamCountdownText.text = string.Format("Next LFJam starts {0}!", startTime);
        }

        nextJamButton.interactable = true;
    }
}
