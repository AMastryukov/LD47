using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class AnimatedImage : MonoBehaviour
{
    [SerializeField] private float fps = 10f;
    [SerializeField] private Sprite[] sprites;

    private Image image;
    private int currentSprite = 0;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    public void Start()
    {
        StartCoroutine(AnimateImage());
    }

    private IEnumerator AnimateImage()
    {
        while(true)
        {
            image.sprite = sprites[currentSprite];

            currentSprite++;
            if (currentSprite >= sprites.Length) { currentSprite = 0; }

            yield return new WaitForSeconds(1f / fps);
        }
    }
}
