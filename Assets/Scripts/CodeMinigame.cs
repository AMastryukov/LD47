using System;
using UnityEngine;
using TMPro;

public class CodeMinigame : MonoBehaviour
{
    public static Action<float> onScore;

    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private TextMeshProUGUI requiredCodeText;

    [Header("Code String Data")]
    [SerializeField] private string[] formatStrings;
    [SerializeField] private string[] contentStrings;

    private string randomCode = "";
    private int[] scoreArray;
    private int previousInputLength = -1;

    private void Start()
    {
        GenerateNewCode();
    }

    private void GenerateNewCode()
    {
        // Make sure it's 20 characters max
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
    }

    public void VerifyInput()
    {
        int currentTypedChar = inputField.text.Length - 1;
        bool isBackspace = previousInputLength >= currentTypedChar;

        previousInputLength = currentTypedChar;

        if (isBackspace || currentTypedChar == -1) { return; }

        if (inputField.text[currentTypedChar].Equals(randomCode[currentTypedChar]))
        {
            if (scoreArray[currentTypedChar] == 1)
            {
                onScore?.Invoke(1f);

                scoreArray[currentTypedChar] = 0;

                // We've reached the end, generate new code string
                if (currentTypedChar == randomCode.Length - 1) { GenerateNewCode(); }
            }
            
        }
        else
        {
            onScore?.Invoke(-1);

            inputField.text = inputField.text.Substring(0, inputField.text.Length - 1);
            VerifyInput();
        }
    }
}
