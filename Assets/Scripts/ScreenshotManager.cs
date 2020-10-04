using UnityEngine;
using System.Collections;
using System;

public class ScreenshotManager : MonoBehaviour
{
    public static Action<Sprite> onScreenshotTaken;

    [SerializeField] private RectTransform captureArea;
    
    private int width;
    private int height;

    private Sprite screenshot;

    public void TakeScreenshot()
    {
        width = System.Convert.ToInt32(captureArea.rect.width);
        height = System.Convert.ToInt32(captureArea.rect.height);

        StartCoroutine(ScreenshotCoroutine());
    }

    private IEnumerator ScreenshotCoroutine()
    {
        yield return new WaitForEndOfFrame();

        Vector2 captureAreaPosition = captureArea.transform.position;
        float startX = captureAreaPosition.x - width / 2;
        float startY = captureAreaPosition.y - height / 2;

        Texture2D texture = new Texture2D(width, height, TextureFormat.RGB24, false);
        Rect rect = new Rect(startX, startY, width, height);

        texture.ReadPixels(rect, 0, 0);
        texture.Apply();

        screenshot = Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100.0f);
        onScreenshotTaken?.Invoke(screenshot);
    }
}