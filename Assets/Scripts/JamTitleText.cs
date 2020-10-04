using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class JamTitleText : MonoBehaviour
{
    private TextMeshProUGUI jamTitleText;

    private void Awake()
    {
        jamTitleText = GetComponent<TextMeshProUGUI>();

        GameManager.onLudumFactorumUpdated += UpdateLFText;
    }

    private void OnDestroy()
    {
        GameManager.onLudumFactorumUpdated -= UpdateLFText;
    }

    private void UpdateLFText(int currentLF)
    {
        jamTitleText.text = "<color=#EE5533>Ludum</color> <color=#F79122>Factorum</color> " + currentLF;
    }
}
