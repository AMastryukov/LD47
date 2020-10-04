using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StartGameDisplay : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputTextBox;
    [SerializeField] private Button startButton;

    private void Awake()
    {
        UpdateButton();
    }

    public void UpdateButton()
    {
        startButton.interactable = inputTextBox.text.Length >= 3;
    }

    public void PressStartButton()
    {
        FindObjectOfType<GameManager>().NewGame(inputTextBox.text);
    }
}
