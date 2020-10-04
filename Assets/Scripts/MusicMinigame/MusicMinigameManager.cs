using System;
using System.Collections.Generic;
using UnityEngine;

public class MusicMinigameManager : MonoBehaviour
{
    // Scoring
    public static Action<float> onGameScored;

    [SerializeField] public float beatHitScore { get; private set; } = 1f;
    [SerializeField] public float beatMissScore { get; private set; } = -1f;

    // TODO: Remove when hooking into main game
    void Start()
    {
        StartMinigame();
    }

    // Start game
    void StartMinigame()
    {
        GetComponent<BeatSpawnerManager>().StartSpawning();
    }

    public void AddScore(float score)
    {
        Debug.Log("Scored: " + score);
        onGameScored?.Invoke(score);
    }
}
