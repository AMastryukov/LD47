using System;
using System.Collections.Generic;
using UnityEngine;

public class MusicMinigame : MonoBehaviour
{
    public static Action<float> onScore;

    [SerializeField] public float beatHitScore = 1f;
    [SerializeField] public float beatMissScore = -1f;

    private void Awake()
    {
        PianoKey.onKeyPressed += CalculateScore;
    }

    void Start()
    {
        StartMinigame();
    }

    public void StartMinigame()
    {
        GetComponent<BeatSpawner>().StartSpawning();
    }

    private void CalculateScore(bool success)
    {
        float score = success ? beatHitScore : beatMissScore;
        onScore?.Invoke(score);
    }
}
