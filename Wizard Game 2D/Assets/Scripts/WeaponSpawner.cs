using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSpawner : MonoBehaviour
{
    public GameObject[] weapons;
    public float xBound;
    public float yBound;
    public int maxWeapons;

    void Start()
    {
        StartCoroutine(SpawnWeapon());
    }

    IEnumerator SpawnWeapon()
    {
        yield return new WaitForSeconds(3);
        Vector2 spawnPoint = new Vector2(Random.Range(-xBound, xBound), Random.Range(-yBound, yBound));
        if(GameObject.FindGameObjectsWithTag("Weapon").Length < maxWeapons)
        {
            Instantiate(weapons[Random.Range(0, weapons.Length)], spawnPoint, Quaternion.identity);
        }

        StartCoroutine(SpawnWeapon());
    }
}
