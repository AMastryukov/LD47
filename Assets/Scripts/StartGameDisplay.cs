using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StartGameDisplay : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputTextBox;
    [SerializeField] private Button startButton;
    [SerializeField] private TextMeshProUGUI themeText;
    [SerializeField] private string[] themes;

    private void Awake()
    {
        UpdateButton();

        GameManager.onLudumFactorumUpdated += UpdateTheme;
    }

    private void OnDestroy()
    {
        GameManager.onLudumFactorumUpdated -= UpdateTheme;
    }

    public void UpdateButton()
    {
        startButton.interactable = inputTextBox.text.Length >= 3;
    }

    public void PressStartButton()
    {
        FindObjectOfType<GameManager>().NewGame(inputTextBox.text);
    }

    private void UpdateTheme(int lf)
    {
        themeText.text = "<b>Theme:</b> " + themes[UnityEngine.Random.Range(0, themes.Length - 1)];
    }
}
