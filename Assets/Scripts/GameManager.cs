using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float defaultTimer = 60f;
    [SerializeField] private float defaultMotivation = 25f;

    public int Art { get; private set; }
    public int Music { get; private set; }
    public int Code { get; private set; }
    public int Inspiration { get; private set; }
    public float Motivation { get; private set; }
    public float Timer { get; private set; }

    public void StartNewGame()
    {
        Art = 0;
        Music = 0;
        Code = 0;

        Inspiration = 0;

        Motivation = defaultMotivation;
        Timer = defaultTimer;
    }

    public void AddArt(int art)
    {
        Art += art;
    }

    public void AddMusic(int music)
    {
        Music += music;
    }

    public void AddCode(int code)
    {
        Code += code;
    }
}
