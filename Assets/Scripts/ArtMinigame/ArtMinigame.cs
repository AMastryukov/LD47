using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ArtMinigame : MonoBehaviour
{
    public static Action<float> onScore;
    public static Action<Sprite> onScreenshotTaken;

    [SerializeField] private RectTransform captureArea;
    [SerializeField] private ArtPixel[] pixels;

    [SerializeField] private Image[] colorImages;
    [SerializeField] private Image[] colorFillImages;

    [Header("Gameplay Values")]
    [SerializeField] private float scorePerPixel = 0.25f;

    private Color selectedColor = Color.white;

    private Color[] colorRequirement = new Color[3];
    private int[] pixelRequirements = new int[3];
    private int[] currentPixelFills = new int[3];

    private bool isActive = false;

    private void Awake()
    {
        ArtPixel.onArtPixelTouched += VerifyPaintAttempt;
    }

    private void OnDestroy()
    {
        ArtPixel.onArtPixelTouched -= VerifyPaintAttempt;
    }

    public void ActivateGame(bool active)
    {
        isActive = active;

        if (isActive) { ResetMinigame(); }
    }

    public void ResetMinigame()
    {
        // Set pixel amount requirements
        pixelRequirements[0] = pixels.Length / 2;
        pixelRequirements[1] = (pixels.Length - pixelRequirements[0]) / 2;
        pixelRequirements[2] = pixels.Length - pixelRequirements[0] - pixelRequirements[1];

        // Reset the current fills
        currentPixelFills[0] = 0;
        currentPixelFills[1] = 0;
        currentPixelFills[2] = 0;

        GenerateColorRequirements();

        colorImages[0].color = colorRequirement[0];
        colorImages[1].color = colorRequirement[1];
        colorImages[2].color = colorRequirement[2];

        colorFillImages[0].color = colorRequirement[0];
        colorFillImages[1].color = colorRequirement[1];
        colorFillImages[2].color = colorRequirement[2];

        selectedColor = Color.white;

        foreach (ArtPixel pixel in pixels)
        {
            pixel.ResetPixel();
        }

        UpdateFillImages();
    }

    public void SetSelectedColor(int colorIndex)
    {
        selectedColor = colorRequirement[colorIndex];
    }

    private void GenerateColorRequirements()
    {
        // TODO: make so these are not too similar to each other
        colorRequirement[0] = new Color(UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f));
        colorRequirement[1] = new Color(UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f));
        colorRequirement[2] = new Color(UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f));
    }

    private void VerifyPaintAttempt(ArtPixel artPixel, Color color)
    {
        // This means that the pixel is already painted the color we need, so we ignore it
        if (color == colorRequirement[0] || color == colorRequirement[1] || color == colorRequirement[2])
        {
            return;
        }

        // This means that we already fulfilled the requirement for the selected color
        for (int i = 0; i < 3; i++)
        {
            if (selectedColor == colorRequirement[i] && currentPixelFills[i] >= pixelRequirements[i])
            {
                return;
            }
        }

        // Ignore white-colored paint
        if (selectedColor == Color.white)
        {
            return;
        }

        onScore?.Invoke(scorePerPixel);
        artPixel.PaintPixel(selectedColor);

        // Fill in the requirements
        for (int i = 0; i < 3; i++) 
        { 
            if (selectedColor == colorRequirement[i]) 
            { 
                currentPixelFills[i]++; 
            } 
        }

        UpdateFillImages();

        // Verify if the artwork is complete (all requirements are fulfilled)
        for (int i = 0; i < 3; i++) 
        { 
            if (currentPixelFills[i] < pixelRequirements[i]) 
            {
                return; 
            } 
        }

        // If we get here, the game is complete
        StartCoroutine(CompleteMinigameCoroutine());
    }

    private void UpdateFillImages()
    {
        colorFillImages[0].fillAmount = (float)currentPixelFills[0] / (float)pixelRequirements[0];
        colorFillImages[1].fillAmount = (float)currentPixelFills[1] / (float)pixelRequirements[1];
        colorFillImages[2].fillAmount = (float)currentPixelFills[2] / (float)pixelRequirements[2];
    }
    private IEnumerator CompleteMinigameCoroutine()
    {
        yield return new WaitForEndOfFrame();

        int width = Convert.ToInt32(captureArea.rect.width);
        int height = Convert.ToInt32(captureArea.rect.height);

        Vector2 captureAreaPosition = captureArea.transform.position;
        float startX = captureAreaPosition.x - width / 2;
        float startY = captureAreaPosition.y - height / 2;

        Texture2D texture = new Texture2D(width, height, TextureFormat.RGB24, false);
        Rect rect = new Rect(startX, startY, width, height);

        texture.ReadPixels(rect, 0, 0);
        texture.Apply();

        Sprite screenshot = Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100.0f);
        onScreenshotTaken?.Invoke(screenshot);

        ResetMinigame();
    }
}
