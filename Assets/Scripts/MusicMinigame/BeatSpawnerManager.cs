using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatSpawnerManager : MonoBehaviour
{
    public GameObject[] beatSpawners;
    public GameObject beat;
    [SerializeField] private float spawnInterval = 0.7f;
    
    public void StartSpawning()
    {
        StartCoroutine(CreateNewBeat());
    }
    
    IEnumerator CreateNewBeat()
    {
        while(true)
        {
            yield return new WaitForSeconds(spawnInterval);

            // Can spawn 1 to 3 beats at once (max in Random.Range is exclusive)
            int beatsToSpawn = Random.Range(1, 4);
            List<int> spawnerColumnsInUse = new List<int>();
            for (int beatNumber = 1; beatNumber <= beatsToSpawn; beatNumber++)
            {
                // Ensure multiple spawns don't happen on the same column
                int beatSpawnerColumn = GenerateSpawnPosition(spawnerColumnsInUse);
                spawnerColumnsInUse.Add(beatSpawnerColumn);

                Transform beatSpawner = beatSpawners[beatSpawnerColumn].transform;
                GameObject spawnedBeat = Instantiate(beat, beatSpawner.position, beatSpawner.rotation);
                spawnedBeat.transform.SetParent(gameObject.transform);
                spawnedBeat.GetComponent<Beat>().Initialize(beatSpawnerColumn);
            }
        }
    }

    // Returns a spawn position not already in use for the current wave
    // spawnerColumnsInUse - List of ints indicating spawner columns in use
    int GenerateSpawnPosition(List<int> spawnerColumnsInUse)
    {
        int beatSpawnerColumn = Random.Range(0, 10);
        if (spawnerColumnsInUse.Contains(beatSpawnerColumn))
        {
            if (beatSpawnerColumn < 5)
            {
                beatSpawnerColumn = Random.Range(beatSpawnerColumn + 1, 10);
            }
            else
            {
                beatSpawnerColumn = Random.Range(0, beatSpawnerColumn);
            }
        }
        return beatSpawnerColumn;
    }
}
