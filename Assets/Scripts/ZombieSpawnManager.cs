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
        Vector3 spawnPoint = GetRandomSpawnPoint();

        Instantiate(zombie, spawnPoint, Quaternion.identity);
    }

    public Vector3 GetRandomSpawnPoint() {
        // Get random position on the scene bounds
        float x = Random.Range(Camera.main.aspect * Camera.main.orthographicSize, -Camera.main.aspect * Camera.main.orthographicSize);
        float y = Random.Range(Camera.main.orthographicSize, -Camera.main.orthographicSize);
        return new Vector3(x, y, 0f);
    }
}
