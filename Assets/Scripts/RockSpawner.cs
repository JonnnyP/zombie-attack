using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockSpawner : MonoBehaviour {

    public GameObject rock;
    public Transform spawnPoint;

    void Start() {
        SpawnRock();
        SpawnRock();
        SpawnRock();
    }

    private void SpawnRock() {
        Vector3 spawnPoint = GetRandomSpawnPoint();

        Instantiate(rock, spawnPoint, Quaternion.identity);
    }

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
