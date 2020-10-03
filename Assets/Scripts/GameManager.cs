using System;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static Action<float> onTimerUpdated;
    public static Action<float> onMotivationUpdated;

    [SerializeField] private float defaultTimer = 48f;
    [SerializeField] private float defaultMotivation = 25f;
    [SerializeField] private float maximumMotivation = 100f;
    [SerializeField] private float motivationLossRate = 2f;
    [SerializeField] private float motivationGainRate = 10f;
    [SerializeField] private float inspirationChance = 0.25f;
    [SerializeField] private float inspirationTimer = 8f;

    public Game CurrentGame { get; private set; }
    public float Motivation { get; private set; }
    public float Timer { get; private set; }
    public float InspiredArtTimer { get; private set; }
    public float InspiredMusicTimer { get; private set; }
    public float InspiredCodeTimer { get; private set; }

    private Coroutine timerCoroutine;
    private Coroutine motivationCoroutine;
    private Coroutine inspirationRollCoroutine;

    private void Start()
    {
        NewGame();
    }

    public void NewGame()
    {
        CurrentGame = new Game();

        Motivation = defaultMotivation;
        Timer = defaultTimer;

        InspiredArtTimer = 0f;
        InspiredMusicTimer = 0f;
        InspiredCodeTimer = 0f;

        timerCoroutine = StartCoroutine(CountdownTimer());
        motivationCoroutine = StartCoroutine(LoseMotivation());
        inspirationRollCoroutine = StartCoroutine(InspirationRoll());
    }

    public void AddFun(float fun)
    {
        if (CurrentGame != null) { CurrentGame.Fun += fun; }
    }

    public void AddGraphics(float graphics)
    {
        if (CurrentGame != null) { CurrentGame.Graphics += graphics; }
    }

    public void AddAudio(float audio)
    {
        if (CurrentGame != null) { CurrentGame.Audio += audio; } 
    }

    private IEnumerator CountdownTimer()
    {
        while (Timer > 0f)
        {
            Timer -= 1f;
            onTimerUpdated?.Invoke(Timer);

            yield return new WaitForSeconds(1f);
        }

        FinishGame(true);
    }

    private IEnumerator LoseMotivation()
    {
        while (Motivation > 0f)
        {
            Motivation -= motivationLossRate * 0.25f;
            onMotivationUpdated?.Invoke(Motivation / maximumMotivation);

            yield return new WaitForSeconds(0.25f);
        }

        FinishGame(false);
    }

    private IEnumerator InspirationRoll()
    {
        while (Timer > 0f)
        {
            yield return new WaitForSeconds(1f);

            // Roll for a chance of inspiration every second
            if (UnityEngine.Random.Range(0f, 1f) < inspirationChance)
            {
                
            }
        }
    }

    private void FinishGame(bool completed)
    {
        StopCoroutine(timerCoroutine);
        StopCoroutine(motivationCoroutine);
        StopCoroutine(inspirationRollCoroutine);

        if (completed) { Debug.Log("You submitted your game!"); }
        else { Debug.Log("You lost all motivation and gave up"); }
    }
}

public class Game
{
    public float Fun { get; set; }
    public float Graphics { get; set; }
    public float Audio { get; set; }

    public Game()
    {
        Fun = 0f;
        Graphics = 0f;
        Audio = 0f;
    }
}
