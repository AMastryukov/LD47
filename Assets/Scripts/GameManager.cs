using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static Action<float> onTimerUpdated;
    public static Action<float> onMotivationUpdated;
    public static Action<float> onFunInspirationTimerUpdated;
    public static Action<float> onGraphicsInspirationTimerUpdated;
    public static Action<float> onAudioInspirationTimerUpdated;
    public static Action<int> onLudumFactorumUpdated;
    public static Action onGameStarted;
    public static Action onGameFinished;

    public static int currentLF = 46;

    public static float audioSkill = 0.0f;
    public static float funSkill = 0.0f;
    public static float graphicsSkill = 0.0f;

    [Header("References")]
    [SerializeField] private MinigameManager minigameManager;
    [SerializeField] private CanvasGroupDisplay startGameCGD;
    [SerializeField] private CanvasGroupDisplay minigameCGD;
    [SerializeField] private CanvasGroupDisplay gameResultCGD;
    [SerializeField] private CanvasGroupDisplay informationBarCGD;
    [SerializeField] private GameResultDisplay gameResultDisplay;
    [SerializeField] private CanvasGroup minigameButtonsCG;

    [Header("Gameplay Values")]
    [SerializeField] private float defaultTimer = 48f;
    [SerializeField] private float defaultMotivation = 25f;
    [SerializeField] private float maximumMotivation = 100f;
    [SerializeField] private float motivationLossRate = 0.5f;
    [SerializeField] private float inspirationChance = 0.25f;
    [SerializeField] private float inspirationTimerIncrease = 3f;
    [SerializeField] private float inspirationTimerMax = 5f;
    [SerializeField] private float skillIncreasePerCent = 0.0025f;

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
        MusicMinigame.onScore += AddAudio;
        ArtMinigame.onScore += AddGraphics;
        ArtMinigame.onScreenshotTaken += AddGameScreenshot;
    }

    private void OnDestroy()
    {
        CodeMinigame.onScore -= AddFun;
        MusicMinigame.onScore -= AddAudio;
        ArtMinigame.onScore -= AddGraphics; 
        ArtMinigame.onScreenshotTaken -= AddGameScreenshot;
    }

    private void Start()
    {
        StartJam();
    }

    public void StartJam()
    {
        minigameManager.CloseAllMinigames();

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

        onGameStarted?.Invoke();
    }

    public void AddFun(float fun)
    {
        if (CurrentGame != null) 
        {
            fun += funSkill;

            if (FunInspirationTimer > 0f) { fun *= 1.5f; }

            CurrentGame.Fun += fun;
            Motivation += fun;
        }
    }

    public void AddGraphics(float graphics)
    {
        if (CurrentGame != null) 
        {
            graphics += graphicsSkill;

            if (GraphicsInspirationTimer > 0f) { graphics *= 1.5f; }

            CurrentGame.Graphics += graphics;
            Motivation += graphics;
        }
    }

    public void AddAudio(float audio)
    {
        if (CurrentGame != null) 
        {
            audio += audioSkill;

            if (AudioInspirationTimer > 0f) { audio *= 1.5f; }

            CurrentGame.Audio += audio;
            Motivation += audio;
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

        minigameButtonsCG.blocksRaycasts = false;

        startGameCGD.CloseDisplay();
        minigameCGD.CloseDisplay();
        informationBarCGD.CloseDisplay();
        gameResultCGD.OpenDisplay();

        minigameManager.CloseAllMinigames();
        gameResultDisplay.ShowGameResult(completed);

        UpdateSkills();

        onGameFinished?.Invoke();
    }

    private void UpdateSkills()
    {
        funSkill += CurrentGame.Fun * skillIncreasePerCent;
        audioSkill += CurrentGame.Audio * skillIncreasePerCent;
        graphicsSkill += CurrentGame.Graphics * skillIncreasePerCent;
    }

    private void AddGameScreenshot(Sprite screenshot)
    {
        CurrentGame.Screenshots.Add(screenshot);
    }
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
    public List<Sprite> Screenshots { get; set; } = new List<Sprite>();

    public Game(string name)
    {
        Name = name;

        Fun = 0f;
        Graphics = 0f;
        Audio = 0f;
    }
}
