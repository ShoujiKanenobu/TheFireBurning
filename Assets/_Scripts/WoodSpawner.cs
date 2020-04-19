using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodSpawner : MonoBehaviour
{
    public BoxCollider2D bc2d;
    public GameObject woodPrefab;
    public GameObject chocoPrefab;
    public GameObject coalPrefab;
    public FirePitController fp;
    public int woodSpawnRate;
    public int startChocoSpawn;
    public int chocoSpawnRate;
    public int startStickSpawn;
    public int coalSpawnRate;
    // Start is called before the first frame update
    void Start()
    {
        bc2d = this.GetComponent<BoxCollider2D>();

        int startingWood = Random.Range(6, 11);
        for (int i = 0; i < startingWood; i++)
        {
            GameObject wood = Instantiate(woodPrefab);
            wood.transform.position = spawnInBox(bc2d);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (fp.fireHP <= 0)
            return;
        if (woodSpawnRate < 1000 && Random.Range(0, 100) == 0)
            woodSpawnRate += Random.Range(0, 5);
        int roll = Random.Range(0, woodSpawnRate);
        if(roll == 0)
        {
            GameObject wood = Instantiate(woodPrefab);
            wood.transform.position = spawnInBox(bc2d);
        }

        if (woodSpawnRate >= startChocoSpawn && Random.Range(0, 10000) == 0)
            chocoSpawnRate -= Random.Range(0, 1);
        int chocoRoll = Random.Range(0, chocoSpawnRate);
        if (woodSpawnRate > startChocoSpawn && chocoRoll == 0)
        {
            GameObject choco = Instantiate(chocoPrefab);
            choco.transform.position = spawnInBox(bc2d);
        }

        if (woodSpawnRate >= startStickSpawn && Random.Range(0, 10000) == 0)
            coalSpawnRate -= Random.Range(0, 1);
        int coalRoll = Random.Range(0, coalSpawnRate);
        if (woodSpawnRate > startStickSpawn && coalRoll == 0)
        {
            GameObject coal = Instantiate(coalPrefab);
            coal.transform.position = spawnInBox(bc2d);
        }

    }

    public Vector3 spawnInBox(BoxCollider2D bc)
    {
        return new Vector3(
            Random.Range(bc.bounds.min.x, bc.bounds.max.x),
            Random.Range(bc.bounds.min.y, bc.bounds.max.y),
            -0.01f
            );
    }
}
