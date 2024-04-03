using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawnManager : MonoBehaviour {

    public GameObject zombie;
    public Transform spawnpoint;
    public float spawnRate;

    void Start() {
        StartSpawning();
    }

    private void StartSpawning() {
        InvokeRepeating("SpawnZombie", spawnRate, spawnRate);
    }
    
    private void SpawnZombie() {
        Vector3 spawnPoint = GetRandomSpawnPoint();

        Instantiate(zombie, spawnPoint, Quaternion.identity);
    }

    public Vector2 GetRandomSpawnPoint() {
        // Get camera's position and size
        Vector2 cameraPosition = Camera.main.transform.position;
        float cameraHalfWidth = Camera.main.aspect * Camera.main.orthographicSize;
        float cameraHalfHeight = Camera.main.orthographicSize;

        float padding = 10f; // Adjust this value based on how far off the camera bounds you want to spawn

        // Calculate random position just outside the camera bounds
        float x = Random.Range(cameraPosition.x - cameraHalfWidth - padding, cameraPosition.x + cameraHalfWidth + padding);
        float y = Random.Range(cameraPosition.y - cameraHalfHeight - padding, cameraPosition.y + cameraHalfHeight + padding);

        return new Vector3(x, y, 0f);
    }
}
