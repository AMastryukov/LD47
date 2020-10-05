using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class TimerDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;

    private void Awake()
    {
        GameManager.onTimerUpdated += UpdateTimerText;
    }

    private void OnDestroy()
    {
        GameManager.onTimerUpdated -= UpdateTimerText;
    }

    private void UpdateTimerText(float newTimer)
    {
        timerText.text = string.Format("<b>COMPO ENDS</b>\n{0:0} hours", newTimer);
    }
}
