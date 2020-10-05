using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class CoffeePot : MonoBehaviour
{
    [SerializeField] private Sprite[] coffeePotSprites;

    private Image coffeePotImage;

    private void Awake()
    {
        coffeePotImage = GetComponent<Image>();
        coffeePotImage.sprite = coffeePotSprites[UnityEngine.Random.Range(0, coffeePotSprites.Length)];
    }
}
