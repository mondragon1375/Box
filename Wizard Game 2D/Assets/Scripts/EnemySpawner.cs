using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public GameObject[] enemies;

    [SerializeField]
    private float spawnRadius = 10.5f;

    //[SerializeField]
    //private float time = 1.5f;

    void Start()
    {
        StartCoroutine(SpawnAnEnemy());
    }

    IEnumerator SpawnAnEnemy()
    {
        // Should be at camera, as being on the edge might spawn in player sight

        Vector2 spawnPos = GameObject.Find("Main Camera").transform.position;
        spawnPos += Random.insideUnitCircle.normalized * spawnRadius;

        if (GameplayManager.instance.spawn)
        {
            float Chance = Random.value;

            if (Chance < 0.1 && GameplayManager.instance.levelNumber >= 3)
                Instantiate(enemies[2], spawnPos, Quaternion.identity);
            else if (Chance < 0.5 && GameplayManager.instance.levelNumber >= 2)
                Instantiate(enemies[1], spawnPos, Quaternion.identity);
            else
                Instantiate(enemies[0], spawnPos, Quaternion.identity);

            //Instantiate(enemies[Random.Range(0, enemies.Length - 1)], spawnPos, Quaternion.identity);
        }
        
        yield return new WaitForSeconds(GameplayManager.instance.spawnTime);
        StartCoroutine(SpawnAnEnemy());
    }
}
