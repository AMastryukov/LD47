using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatSpawner : MonoBehaviour
{
    [SerializeField] private Transform[] beatSpawners;
    [SerializeField] private Transform beatParent;
    [SerializeField] private GameObject beatPrefab;
    [SerializeField] private float ludumBPM = 150f;
    [SerializeField] private float factorumBPM = 140f;

    private Coroutine spawnCoroutine;
    private AudioManager audioManager;

    private float bpm = 140f;

    private void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }

    public void StartSpawning()
    {
        if (audioManager.CurrentSong.name == "ludum") { bpm = ludumBPM; }
        else { bpm = factorumBPM; }

        spawnCoroutine = StartCoroutine(CreateNewBeat());
    }

    public void StopSpawning()
    {
        if (spawnCoroutine == null) { return; }

        StopCoroutine(spawnCoroutine);
    }
    
    IEnumerator CreateNewBeat()
    {
        while(true)
        {
            GameObject spawnedBeat = Instantiate(beatPrefab, beatParent);
            spawnedBeat.transform.position = beatSpawners[Random.Range(0, beatSpawners.Length)].position;

            yield return new WaitForSeconds(60f / bpm);
        }
    }
}
