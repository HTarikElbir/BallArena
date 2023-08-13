using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefabs;
    public GameObject powerupPrefabs;
    private float spawnRange = 5.5f;
    private int waveLevel = 1;
    public int enemyCount;
    public TextMeshProUGUI levelUp;
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
            levelUp.gameObject.SetActive(true);
            StartCoroutine(LevelupCountDownRoutine());
            waveLevel++;
            EnemyWaveControl(waveLevel);
            Instantiate(powerupPrefabs, SpawnPosition(), powerupPrefabs.transform.rotation);

        }
        
    }
    IEnumerator LevelupCountDownRoutine()
    {
        yield return new WaitForSeconds(3);
        levelUp.gameObject.SetActive(false);
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
