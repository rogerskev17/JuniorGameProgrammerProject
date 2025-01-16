using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] spawns;
    private float spawnXMinEnemy = 10.0f;
    private float spawnXMaxEnemy = 20.0f;
    private float spawnZMinEnemy = 2.5f;
    private float spawnZMaxEnemy = 12.0f;
    private float spawnZMinPowerup = 15.0f;
    private float spawnZMaxPowerup = 20.0f;
    private float spawnXMinPowerup = 0.0f;
    private float spawnXMaxPowerup = 5.0f;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        float startDelay = 5.0f;
        float spawnInterval = 5.0f;
        InvokeRepeating("SpawnRandomEnemy", startDelay, spawnInterval);
    }

    // Update is called once per frame
    void Update()
    {
        player = GameObject.Find("Player");
    }

    void SpawnRandomEnemy()
    {
        int spawnIndex = UnityEngine.Random.Range(0, spawns.Length);
        if (spawnIndex < 2)
        {
            Vector3 spawnLoc = new Vector3(UnityEngine.Random.Range(spawnXMinEnemy, spawnXMaxEnemy), 0.05f, UnityEngine.Random.Range(spawnZMinEnemy, spawnZMaxEnemy));
            Vector3 lookDirection = (spawnLoc - player.transform.position).normalized;
            Instantiate(spawns[spawnIndex], spawnLoc, Quaternion.LookRotation(lookDirection, Vector3.up));

            Debug.Log("Enemy " + (spawnIndex + 1) + " Spawned");
        }
        else
        {
            Vector3 spawnLoc = new Vector3(UnityEngine.Random.Range(spawnXMinPowerup, spawnXMaxPowerup), 1.5f, UnityEngine.Random.Range(spawnZMinPowerup, spawnZMaxPowerup));
            Instantiate(spawns[spawnIndex], spawnLoc, spawns[spawnIndex].transform.rotation);
            Debug.Log("Powerup spawned");
        }
        
    }
}
