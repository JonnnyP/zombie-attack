using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawnManager : MonoBehaviour {

    public GameObject zombie;
    public float maxX;
    public float maxY;
    public Transform spawnpoint;
    public float spawnRate;

    // bool gameStarted = false;
    
    void Start() {
        StartSpawning();
    }

    void Update() {
        
    }

    private void StartSpawning() {
        InvokeRepeating("SpawnZombie", 0.5f, spawnRate);
    }


    private void SpawnZombie() {
        Vector3 spawnPos = spawnpoint.position;
        spawnPos.x = Random.Range(-maxX, maxX);
        spawnPos.y = Random.Range(-maxY, maxY);

        Instantiate(zombie, spawnPos, Quaternion.identity);
    }
}
