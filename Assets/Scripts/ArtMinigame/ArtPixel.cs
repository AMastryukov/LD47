using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Image))]
public class ArtPixel : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler
{
    public static Action<ArtPixel, Color> onArtPixelTouched;

    private Image pixelImage;

    private void Awake()
    {
        pixelImage = GetComponent<Image>();
    }

    public void PaintPixel(Color color)
    {
        pixelImage.color = color;
    }

    public void ResetPixel()
    {
        pixelImage.color = Color.white;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (Input.GetMouseButton(0))
        {
            onArtPixelTouched?.Invoke(this, pixelImage.color);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        onArtPixelTouched?.Invoke(this, pixelImage.color);
    }
}
