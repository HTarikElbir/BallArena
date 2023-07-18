using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefabs;
    public GameObject powerupPrefabs;
    private float spawnRange = 5.5f;
    private int waveLevel = 1;
    public int enemyCount;
    // Start is called before the first frame update
    void Start()
    {
        EnemyWaveControl(1);
        Instantiate(powerupPrefabs, SpawnPosition(), powerupPrefabs.transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        enemyCount = FindObjectsOfType<Enemy>().Length;

        if (enemyCount == 0)
        {
            waveLevel++;
            EnemyWaveControl(waveLevel);
            Instantiate(powerupPrefabs, SpawnPosition(), powerupPrefabs.transform.rotation);

        }
    }
    void EnemyWaveControl(int enemiesToSpawn)
    {
        for(int i = 0; i < enemiesToSpawn; i++)
        {
            Instantiate(enemyPrefabs, SpawnPosition(), enemyPrefabs.transform.rotation);
        }
    }
    private Vector3 SpawnPosition()
    {
        float spawnPositionX = Random.Range(-spawnRange,spawnRange);
        float spawnPositionZ = Random.Range(-spawnRange, spawnRange);
        Vector3 randomPosition = new Vector3(spawnPositionX, 1.5f, spawnPositionZ);
        return randomPosition;
    }
}
