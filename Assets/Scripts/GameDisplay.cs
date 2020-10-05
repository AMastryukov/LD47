using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameDisplay : MonoBehaviour
{
    [Header("Text References")]
    [SerializeField] private TextMeshProUGUI funText;
    [SerializeField] private TextMeshProUGUI graphicsText;
    [SerializeField] private TextMeshProUGUI audioText;

    [Header("Image References")]
    [SerializeField] private Image funInspirationImage;
    [SerializeField] private Image graphicsInspirationImage;
    [SerializeField] private Image audioInspirationImage;

    private void Awake()
    {
        Game.onFunUpdated += UpdateFunText;
        Game.onGraphicsUpdated += UpdateGraphicsText;
        Game.onAudioUpdated += UpdateAudioText;

        GameManager.onFunInspirationTimerUpdated += UpdateFunInspirationImage;
        GameManager.onGraphicsInspirationTimerUpdated += UpdateGraphicsInspirationImage;
        GameManager.onAudioInspirationTimerUpdated += UpdateAudioInspirationImage;
    }

    private void OnDestroy()
    {
        Game.onFunUpdated -= UpdateFunText;
        Game.onGraphicsUpdated -= UpdateGraphicsText;
        Game.onAudioUpdated -= UpdateAudioText;

        GameManager.onFunInspirationTimerUpdated -= UpdateFunInspirationImage;
        GameManager.onGraphicsInspirationTimerUpdated -= UpdateGraphicsInspirationImage;
        GameManager.onAudioInspirationTimerUpdated -= UpdateAudioInspirationImage;
    }

    private void UpdateFunText(float fun)
    {
        funText.text = ((int)fun).ToString();
    }

    private void UpdateGraphicsText(float graphics)
    {
        graphicsText.text = ((int)graphics).ToString();
    }

    private void UpdateAudioText(float audio)
    {
        audioText.text = ((int)audio).ToString();
    }

    private void UpdateFunInspirationImage(float inspirationTimer)
    {
        funInspirationImage.enabled = inspirationTimer > 0f;
    }

    private void UpdateGraphicsInspirationImage(float inspirationTimer)
    {
        graphicsInspirationImage.enabled = inspirationTimer > 0f;
    }

    private void UpdateAudioInspirationImage(float inspirationTimer)
    {
        audioInspirationImage.enabled = inspirationTimer > 0f;
    }
}
