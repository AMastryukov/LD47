using System;
using System.Collections.Generic;
using UnityEngine;

public class MusicMinigame : MonoBehaviour
{
    public static Action<float> onScore;

    [SerializeField] public float beatHitScore = 1f;
    [SerializeField] public float beatMissScore = -2f;

    private AudioManager audioManager;
    private BeatSpawner beatSpawner;

    private bool isActive = false;

    private void Awake()
    {
        GameManager.onGameStarted += StartMinigame;
        GameManager.onGameFinished += EndMinigame;

        PianoKey.onKeyPressed += HandleKeyPress;

        audioManager = FindObjectOfType<AudioManager>();
        beatSpawner = GetComponent<BeatSpawner>();
    }

    private void OnDestroy()
    {
        GameManager.onGameStarted -= StartMinigame;
        GameManager.onGameFinished -= EndMinigame;

        PianoKey.onKeyPressed -= HandleKeyPress;
    }

    public void ActivateGame(bool active)
    {
        isActive = active;
    }

    public void StartMinigame()
    {
        beatSpawner.StartSpawning();
    }

    public void EndMinigame()
    {
        beatSpawner.StopSpawning();
    }

    private void HandleKeyPress(bool success, KeyCode keyCode)
    {
        if (!isActive) { return; }

        switch(keyCode)
        {
            case KeyCode.Q:
                audioManager?.PlayPianoNote(0, success);
                break;

            case KeyCode.W:
                audioManager?.PlayPianoNote(1, success);
                break;

            case KeyCode.E:
                audioManager?.PlayPianoNote(2, success);
                break;

            case KeyCode.R:
                audioManager?.PlayPianoNote(3, success);
                break;
        }

        float score = success ? beatHitScore : beatMissScore;
        onScore?.Invoke(score);
    }
}
