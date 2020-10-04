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
    public static Action<int> onLudumFactorumUpdated;

    public static int currentLF = 46;

    [Header("References")]
    [SerializeField] private CanvasGroupDisplay startGameCGD;
    [SerializeField] private CanvasGroupDisplay minigameCGD;
    [SerializeField] private CanvasGroupDisplay gameResultCGD;
    [SerializeField] private CanvasGroupDisplay informationBarCGD;
    [SerializeField] private GameResultDisplay gameResultDisplay;
    [SerializeField] private CanvasGroup minigameButtonsCG;

    [Header("Soundtrack")]
    [SerializeField] private AudioSource soundtrackSource;
    [SerializeField] private AudioClip[] soundtracks;

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

    private void Awake()
    {
        CodeMinigame.onScore += AddFun;
    }

    private void OnDestroy()
    {
        CodeMinigame.onScore -= AddFun;
    }

    private void Start()
    {
        StartJam();
    }

    public void StartJam()
    {
        informationBarCGD.CloseDisplay();
        startGameCGD.OpenDisplay();
        minigameCGD.CloseDisplay();
        gameResultCGD.CloseDisplay();

        minigameButtonsCG.blocksRaycasts = false;

        currentLF++;
        onLudumFactorumUpdated?.Invoke(currentLF);
    }

    public void NewGame(string gameName = "Untitled")
    {
        CurrentGame = new Game(gameName);

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
        informationBarCGD.OpenDisplay();

        minigameButtonsCG.blocksRaycasts = true;

        // Select a random soundtrack and play it
        soundtrackSource.clip = soundtracks[UnityEngine.Random.Range(0, soundtracks.Length)];
        soundtrackSource.Play();
    }

    public void AddFun(float fun)
    {
        if (CurrentGame != null) 
        { 
            if (FunInspirationTimer > 0f && fun > 0f) { fun *= 2f; }

            CurrentGame.Fun += fun;
            Motivation += motivationGainRate * Mathf.Sign(fun);
        }
    }

    public void AddGraphics(float graphics)
    {
        if (CurrentGame != null) 
        {
            if (GraphicsInspirationTimer > 0f && graphics > 0f) { graphics *= 2f; }

            CurrentGame.Graphics += graphics;
            Motivation += motivationGainRate * Mathf.Sign(graphics);
        }
    }

    public void AddAudio(float audio)
    {
        if (CurrentGame != null) 
        {
            if (AudioInspirationTimer > 0f && audio > 0f) { audio *= 2f; }

            CurrentGame.Audio += audio;
            Motivation += motivationGainRate * Mathf.Sign(audio);
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

    private IEnumerator FadeOutSoundtrack()
    {
        float soundtrackFadeOutTime = 1f;

        while (soundtrackSource.volume > 0f)
        {
            soundtrackSource.volume -= 0.025f;

            yield return new WaitForSeconds(soundtrackFadeOutTime * 0.025f);
        }

        soundtrackSource.Stop();
        soundtrackSource.volume = 1f;
    }

    private void FinishGame(bool completed)
    {
        StopCoroutine(timerCoroutine);
        StopCoroutine(motivationCoroutine);
        StopCoroutine(inspirationCoroutine);

        startGameCGD.CloseDisplay();
        minigameCGD.CloseDisplay();
        informationBarCGD.CloseDisplay();

        gameResultCGD.OpenDisplay();

        gameResultDisplay.ShowGameResult(completed);

        minigameButtonsCG.blocksRaycasts = false;

        StartCoroutine(FadeOutSoundtrack());
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

    public string Name { get; private set; }
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

    public Game(string name)
    {
        Name = name;

        Fun = 0f;
        Graphics = 0f;
        Audio = 0f;
    }
}
