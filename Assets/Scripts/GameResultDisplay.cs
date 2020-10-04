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

    [Header("Gameplay Values")]
    [SerializeField] private int jamEntries = 2000;
    [SerializeField] private float perfectScore = 1000f;

    private CanvasGroupDisplay canvasGroupDisplay;

    private int funPlacement = 0;
    private int graphicsPlacement = 0;
    private int audioPlacement = 0;
    private int overallPlacement = 0;

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

        CalculateGameScore(gm.CurrentGame);

        overallScoreText.text = "Overall: \t" + FormatPlacementString(overallPlacement);
        funScoreText.text = "Fun: \t\t" + FormatPlacementString(funPlacement);
        graphicsScoreText.text = "Graphics: \t" + FormatPlacementString(graphicsPlacement);
        audioScoreText.text = "Audio: \t" + FormatPlacementString(audioPlacement);

        // TODO: display the images stored in the game
    }

    private void CalculateGameScore(Game game)
    {
        funPlacement = PlacementFromScore(game.Fun);
        graphicsPlacement = PlacementFromScore(game.Graphics);
        audioPlacement = PlacementFromScore(game.Audio);

        overallPlacement = Random.Range(
            Mathf.Min(funPlacement, graphicsPlacement, audioPlacement), 
            Mathf.Max(funPlacement, graphicsPlacement, audioPlacement));
    }

    private string FormatPlacementString(int placement)
    {
        string placementString = placement.ToString();
        char lastChar = placementString[placementString.Length - 1];

        string formattedString = placementString;

        switch(lastChar)
        {
            case '1':
                formattedString += "st";
                break;

            case '2':
                formattedString += "nd";
                break;

            case '3':
                formattedString += "rd";
                break;

            default:
                formattedString += "th";
                break;
        }

        return formattedString;
    }

    private int PlacementFromScore(float score)
    {
        return Mathf.Max(1, jamEntries - (int)((score / perfectScore) * jamEntries));
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
