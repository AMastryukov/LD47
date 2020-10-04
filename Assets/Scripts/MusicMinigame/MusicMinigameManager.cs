using System;
using System.Collections.Generic;
using UnityEngine;

public class MusicMinigameManager : MonoBehaviour
{
    public static Action<float> onGameScored;
    public Transform[] keys;
    private Queue<Transform>[] beats;

    // Scoring
    [SerializeField] private int perfectScore = 10;
    [SerializeField] private int greatScore = 7;
    [SerializeField] private int goodScore = 5;
    [SerializeField] private int badScore = 2;
    [SerializeField] private int awfulScore = 0;
    [SerializeField] private int missScore = -5;

    // TODO: Remove when hooking into main game
    void Start()
    {
        startMinigame();
    }

    // Start game
    void startMinigame()
    {
        beats = new Queue<Transform>[10];
        for (int i = 0; i < beats.Length; i++)
        {
            beats[i] = new Queue<Transform>();
        }
        GetComponent<BeatSpawnerManager>().StartSpawning();
    }

    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (beats[0].Count == 0) { onGameScored?.Invoke(missScore); return; }
            Transform relativeBeat = beats[0].Dequeue();
            Debug.Log(Math.Abs(keys[0].position.x - relativeBeat.position.x));
            Destroy(relativeBeat.gameObject);
        }
    }

    // Register a beat object belonging to key
    public void RegisterBeat(Transform beat, int key)
    {
        // Debug.Log(key);
        // Debug.Log(beats[key].ToString());
        Debug.Log(beats == null);
        beats[key].Enqueue(beat); // ERROR here
    }

    // Remove the oldest beat present on screen for key
    public void RemoveOldestBeat(int key)
    {
        beats[key].Dequeue();
    }
}
