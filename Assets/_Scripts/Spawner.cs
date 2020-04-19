using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject enemyTarget;
    public int EnemySpawnRate = 1000;
    public GameObject[] spawnLocations;
    public FirePitController fp;

    private void Start()
    {
        int startingWood = Random.Range(6, 10);
    }
    void Update()
    {
        if (fp.fireHP <= 0)
            return;
        if (EnemySpawnRate > 200 && Random.Range(0, 1000) == 0)
            EnemySpawnRate -= Random.Range(0, 5);
        float roll = Random.Range(0, EnemySpawnRate);
        if(roll == 0){
            int location = Random.Range(0, 4);
            GameObject newEnemy = Instantiate(enemyPrefab);
            newEnemy.transform.position = spawnLocations[location].transform.position;
            newEnemy.GetComponent<EnemyController>().target = enemyTarget;
            switch (location)
            {
                case 0: case 2: 
                    newEnemy.transform.position = newEnemy.transform.position + new Vector3(0, Random.Range(-15, 15), 0);
                    break;
                case 1: case 3:
                    newEnemy.transform.position = newEnemy.transform.position + new Vector3(Random.Range(-15, 15), 0, 0);
                    break;
            }

        }
    }
}
