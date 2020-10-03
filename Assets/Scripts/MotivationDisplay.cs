using System;
using UnityEngine;
using UnityEngine.UI;

public class MotivationDisplay : MonoBehaviour
{
    [SerializeField] private Image motivationBarFill;

    private void Awake()
    {
        GameManager.onMotivationUpdated += UpdateMotivationText;
    }

    private void OnDestroy()
    {
        GameManager.onMotivationUpdated -= UpdateMotivationText;
    }

    private void UpdateMotivationText(float newMotivation)
    {
        motivationBarFill.fillAmount = newMotivation;
        motivationBarFill.color = new Color(2f * (1f - newMotivation), 2f * newMotivation, 0f);
    }
}
