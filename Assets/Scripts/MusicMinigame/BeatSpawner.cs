using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatSpawner : MonoBehaviour
{
    [SerializeField] private Transform[] beatSpawners;
    [SerializeField] private Transform beatParent;
    [SerializeField] private GameObject beatPrefab;
    [SerializeField] private float beatsPerMinute = 140f;
    
    public void StartSpawning()
    {
        StartCoroutine(CreateNewBeat());
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
