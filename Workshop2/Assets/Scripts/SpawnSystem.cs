using System.Collections;
using UnityEngine;

public class SpawnSystem : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float minSpawnDelay = 1.0f;
    public float maxSpawnDelay = 3.0f;
        public Transform[] spawnPoints;

    void Start()
    {
        StartCoroutine(SpawnEnemies());


    }
    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            float delay = Random.Range(minSpawnDelay, maxSpawnDelay);
            yield return new WaitForSeconds(delay);

            int spawnIndex = Random.Range(0, spawnPoints.Length);
            
            Vector3 spawnPosition = spawnPoints[spawnIndex].position;

            Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        }
    }
}
