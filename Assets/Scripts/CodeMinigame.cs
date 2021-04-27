using System;
using UnityEngine;
using TMPro;

public class CodeMinigame : MonoBehaviour
{
    public static Action<float> onScore;

    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private TextMeshProUGUI requiredCodeText;
    [SerializeField] private TextMeshProUGUI feedbackText;
    [SerializeField] private Color successColor;
    [SerializeField] private Color errorColor;

    [Header("Gameplay Values")]
    [SerializeField] private float successScore = 0.75f;
    [SerializeField] private float failScore = -2f;

    [Header("Code String Data")]
    [SerializeField] private string[] formatStrings;
    [SerializeField] private string[] contentStrings;

    private string randomCode = "";
    private int[] scoreArray;
    private int previousInputLength = -1;

    private AudioManager audioManager;

    private bool isActive = false;

    private void Awake()
    {
        GameManager.onGameFinished += GenerateNewCode;

        audioManager = FindObjectOfType<AudioManager>();
    }

    private void OnDestroy()
    {
        GameManager.onGameFinished -= GenerateNewCode;
    }

    private void Start()
    {
        GenerateNewCode();
    }

    public void ActivateGame(bool active)
    {
        isActive = active;
        inputField.enabled = isActive;

        if (isActive) { inputField.Select(); }
    }

    private void GenerateNewCode()
    {
        string randFormatString = formatStrings[UnityEngine.Random.Range(0, formatStrings.Length)];
        string randContentString = contentStrings[UnityEngine.Random.Range(0, contentStrings.Length)];
        randomCode = string.Format(randFormatString, randContentString);

        scoreArray = new int[randomCode.Length];
        for (int i = 0; i < scoreArray.Length; i++)
        {
            scoreArray[i] = 1;
        }

        previousInputLength = -1;
        requiredCodeText.text = randomCode;
        inputField.text = "";

        feedbackText.color = successColor;
        feedbackText.text = "No issues found";
    }

    public void VerifyInput()
    {
        int currentTypedChar = inputField.text.Length - 1;
        bool isBackspace = previousInputLength >= currentTypedChar;

        previousInputLength = currentTypedChar;

        if (isBackspace || currentTypedChar == -1) { return; }

        audioManager?.PlayRandomClick();

        if (inputField.text[currentTypedChar].Equals(randomCode[currentTypedChar]))
        {
            if (scoreArray[currentTypedChar] == 1)
            {
                onScore?.Invoke(successScore);

                feedbackText.color = successColor;
                feedbackText.text = "No issues found";

                scoreArray[currentTypedChar] = 0;

                // We've reached the end, generate new code string
                if (currentTypedChar == randomCode.Length - 1) { GenerateNewCode(); }
            }
        }
        else
        {
            onScore?.Invoke(failScore);

            feedbackText.color = errorColor;
            feedbackText.text = "Error at character " + currentTypedChar;

            inputField.text = inputField.text.Substring(0, inputField.text.Length - 1);
            VerifyInput();
        }
    }
}
