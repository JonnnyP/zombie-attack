using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawnManager : MonoBehaviour {

    public GameObject zombie;
    public Transform spawnpoint;
    public float spawnRate;

    void Start() {
        // StartSpawning();
    }

    private void StartSpawning() {
        InvokeRepeating("SpawnZombie", 0.5f, spawnRate);
    }
    
    private void SpawnZombie() {
        Vector3 spawnPoint = GetRandomSpawnPoint();

        Instantiate(zombie, spawnPoint, Quaternion.identity);
    }

    // public Vector3 GetRandomSpawnPoint() {
    //     float padding = 1f; // Adjust this value based on how far off the camera bounds you want to spawn

    //     // Get random position just outside the scene bounds
    //     float x = Random.Range(-Camera.main.aspect * Camera.main.orthographicSize - padding, Camera.main.aspect * Camera.main.orthographicSize + padding);
    //     float y = Random.Range(-Camera.main.orthographicSize - padding, Camera.main.orthographicSize + padding);

    //     return new Vector3(x, y, 0f);
    // }

    public Vector3 GetRandomSpawnPoint() {

        float padding = 1f; // Adjust this value based on how far off the camera bounds you want to spawn
        float cameraX = ViewportHandler.Instance.camera.transform.position.x;
        float cameraY = ViewportHandler.Instance.camera.transform.position.y;
        float cameraOrthoSize = ViewportHandler.Instance.camera.orthographicSize;
        float cameraAspect = ViewportHandler.Instance.camera.aspect;

        // Get random position just outside the scene bounds
        float x = Random.Range(cameraX - cameraAspect * cameraOrthoSize - padding, cameraX + cameraAspect * cameraOrthoSize + padding);
        float y = Random.Range(cameraY - cameraOrthoSize - padding, cameraY + cameraOrthoSize + padding);

        return new Vector3(x, y, 0f);
    }
}
