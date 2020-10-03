using System;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static Action<float> onTimerUpdated;
    public static Action<float> onMotivationUpdated;
    public static Action<float> onFunInspirationTimerUpdated;
    public static Action<float> onGraphicsInspirationTimerUpdated;
    public static Action<float> onAudioInspirationTimerUpdated;

    [Header("References")]
    [SerializeField] private CanvasGroupDisplay startGameCGD;
    [SerializeField] private CanvasGroupDisplay minigameCGD;
    [SerializeField] private CanvasGroupDisplay gameResultCGD;
    [SerializeField] private GameResultDisplay gameResultDisplay;
    [SerializeField] private CanvasGroup minigameButtonsCG;

    [Header("Gameplay Values")]
    [SerializeField] private float defaultTimer = 48f;
    [SerializeField] private float defaultMotivation = 25f;
    [SerializeField] private float maximumMotivation = 100f;
    [SerializeField] private float motivationLossRate = 2f;
    [SerializeField] private float motivationGainRate = 10f;
    [SerializeField] private float inspirationChance = 0.25f;
    [SerializeField] private float inspirationTimerIncrease = 3f;
    [SerializeField] private float inspirationTimerMax = 5f;

    private float _motivation;
    private float _timer;
    private float _funInspirationTimer;
    private float _graphicsInspirationTimer;
    private float _audioInspirationTimer;

    public Game CurrentGame { get; private set; }
    public float Motivation 
    {
        get { return _motivation; }
        private set { _motivation = Mathf.Clamp(value, 0f, maximumMotivation); onMotivationUpdated?.Invoke(_motivation / maximumMotivation); }
    }
    public float Timer 
    {
        get { return _timer; }
        private set { _timer = Mathf.Max(value, 0f); onTimerUpdated?.Invoke(_timer); }
    }
    public float FunInspirationTimer
    {
        get { return _funInspirationTimer; }
        private set { _funInspirationTimer = Mathf.Clamp(value, 0f, inspirationTimerMax); onFunInspirationTimerUpdated?.Invoke(_funInspirationTimer); }
    }
    public float GraphicsInspirationTimer
    {
        get { return _graphicsInspirationTimer; }
        private set { _graphicsInspirationTimer = Mathf.Clamp(value, 0f, inspirationTimerMax); onGraphicsInspirationTimerUpdated?.Invoke(_graphicsInspirationTimer); }
    }
    public float AudioInspirationTimer
    {
        get { return _audioInspirationTimer; }
        private set { _audioInspirationTimer = Mathf.Clamp(value, 0f, inspirationTimerMax); onAudioInspirationTimerUpdated?.Invoke(_audioInspirationTimer); }
    }

    private Coroutine timerCoroutine;
    private Coroutine motivationCoroutine;
    private Coroutine inspirationCoroutine;

    private void Start()
    {
        startGameCGD.OpenDisplay();
        minigameCGD.CloseDisplay();
        gameResultCGD.CloseDisplay();

        minigameButtonsCG.blocksRaycasts = false;
    }

    public void NewGame()
    {
        CurrentGame = new Game();

        Motivation = defaultMotivation;
        Timer = defaultTimer;

        FunInspirationTimer = 0f;
        GraphicsInspirationTimer = 0f;
        AudioInspirationTimer = 0f;

        timerCoroutine = StartCoroutine(TimerCoroutine());
        motivationCoroutine = StartCoroutine(MotivationCoroutine());
        inspirationCoroutine = StartCoroutine(InspirationCoroutine());

        startGameCGD.CloseDisplay();
        gameResultCGD.CloseDisplay();

        minigameCGD.OpenDisplay();

        minigameButtonsCG.blocksRaycasts = true;
    }

    public void AddFun(float fun)
    {
        if (CurrentGame != null) 
        { 
            if (FunInspirationTimer > 0f) { fun *= 2f; }

            CurrentGame.Fun += fun;
            Motivation += motivationGainRate;
        }
    }

    public void AddGraphics(float graphics)
    {
        if (CurrentGame != null) 
        {
            if (GraphicsInspirationTimer > 0f) { graphics *= 2f; }

            CurrentGame.Graphics += graphics;
            Motivation += motivationGainRate;
        }
    }

    public void AddAudio(float audio)
    {
        if (CurrentGame != null) 
        {
            if (AudioInspirationTimer > 0f) { audio *= 2f; }

            CurrentGame.Audio += audio;
            Motivation += motivationGainRate;
        }
    }

    private IEnumerator TimerCoroutine()
    {
        while (Timer > 0f)
        {
            Timer -= 1f;

            yield return new WaitForSeconds(1f);
        }

        FinishGame(true);
    }

    private IEnumerator MotivationCoroutine()
    {
        while (Motivation > 0f)
        {
            Motivation -= motivationLossRate * 0.25f;

            yield return new WaitForSeconds(0.25f);
        }

        FinishGame(false);
    }

    private IEnumerator InspirationCoroutine()
    {
        float coroutineUpdateTime = 0.25f;

        while (Timer > 0f)
        {
            yield return new WaitForSeconds(coroutineUpdateTime);

            // Roll for a chance of inspiration every second
            if (UnityEngine.Random.Range(0f, 1f) < inspirationChance * coroutineUpdateTime)
            {
                int inspirationRoll = UnityEngine.Random.Range(0, 3);

                switch(inspirationRoll)
                {
                    case 0:
                        FunInspirationTimer += inspirationTimerIncrease;
                        break;

                    case 1:
                        GraphicsInspirationTimer += inspirationTimerIncrease;
                        break;

                    case 2:
                        AudioInspirationTimer += inspirationTimerIncrease;
                        break;
                }
            }

            FunInspirationTimer -= coroutineUpdateTime;
            GraphicsInspirationTimer -= coroutineUpdateTime;
            AudioInspirationTimer -= coroutineUpdateTime;
        }
    }

    private void FinishGame(bool completed)
    {
        StopCoroutine(timerCoroutine);
        StopCoroutine(motivationCoroutine);
        StopCoroutine(inspirationCoroutine);

        startGameCGD.CloseDisplay();
        minigameCGD.CloseDisplay();

        gameResultCGD.OpenDisplay();

        gameResultDisplay.ShowGameResult(completed);

        minigameButtonsCG.blocksRaycasts = false;
    }

    #region Fake Minigame Methods
    public void PlayCodingMinigame()
    {
        AddFun(1f);
    }

    public void PlayArtMinigame()
    {
        AddGraphics(1f);
    }

    public void PlayMusicMinigame()
    {
        AddAudio(1f);
    }
    #endregion
}

public class Game
{
    public static Action<float> onFunUpdated;
    public static Action<float> onGraphicsUpdated;
    public static Action<float> onAudioUpdated;

    private float _fun;
    private float _graphics;
    private float _audio;

    public float Fun 
    {
        get { return _fun; }
        set { _fun = Mathf.Max(0f, value); onFunUpdated?.Invoke(_fun); }
    }
    public float Graphics 
    {
        get { return _graphics; }
        set { _graphics = Mathf.Max(0f, value); onGraphicsUpdated?.Invoke(_graphics); }
    }
    public float Audio
    {
        get { return _audio; }
        set { _audio = Mathf.Max(0f, value); onAudioUpdated?.Invoke(_audio); }
    }

    public Game()
    {
        Fun = 0f;
        Graphics = 0f;
        Audio = 0f;
    }
}
