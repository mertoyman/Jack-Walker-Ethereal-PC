using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]Transform[] spawnPoint;
    [SerializeField] GameObject[] enemies;
    [SerializeField] EnemyHealth enemyHealth;
    [SerializeField] int singleSpawnRound;
    [SerializeField] int duoSpawnRound;
    [SerializeField] int timeBetweenSpawns = 3;
    
    public int enemyCount = 0;

    private void Start()
    {
        StartCoroutine("Spawn");
    }

    private IEnumerator Spawn()
    {

        while (true)
        {
            yield return new WaitForSeconds(timeBetweenSpawns);
            if(singleSpawnRound > 0 && enemyCount <=8)
            {

                Instantiate(enemies[Random.Range(0, enemies.Length)], spawnPoint[0].position, Quaternion.identity);
                singleSpawnRound--;
                enemyCount++;
            }
            else if(duoSpawnRound > 0 && enemyCount <= 7)
            {
                Instantiate(enemies[Random.Range(0, enemies.Length)], spawnPoint[0].position, Quaternion.identity);
                Instantiate(enemies[Random.Range(0, enemies.Length)], spawnPoint[1].position, Quaternion.identity);
                duoSpawnRound--;
                enemyCount += 2;
            }
            else if(enemyCount <= 6)
            {
                Instantiate(enemies[Random.Range(0, enemies.Length)], spawnPoint[0].position, Quaternion.identity);
                Instantiate(enemies[Random.Range(0, enemies.Length)], spawnPoint[1].position, Quaternion.identity);
                Instantiate(enemies[Random.Range(0, enemies.Length)], spawnPoint[2].position, Quaternion.identity);
                enemyCount += 3;
            }

        }
    }
}
