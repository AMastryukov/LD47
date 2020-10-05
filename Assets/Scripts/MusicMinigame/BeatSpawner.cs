using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatSpawner : MonoBehaviour
{
    [SerializeField] private Transform[] beatSpawners;
    [SerializeField] private Transform beatParent;
    [SerializeField] private GameObject beatPrefab;
    [SerializeField] private float beatsPerMinute = 140f;

    private Coroutine spawnCoroutine;
    private AudioManager audioManager;

    private void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }

    public void StartSpawning()
    {
        if (audioManager.CurrentSong.name == "ludum") { beatsPerMinute = 150f; }
        else { beatsPerMinute = 140f; }

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
            yield return new WaitForSeconds(60f / beatsPerMinute);

            GameObject spawnedBeat = Instantiate(beatPrefab, beatParent);
            spawnedBeat.transform.position = beatSpawners[Random.Range(0, beatSpawners.Length)].position;
        }
    }
}
