using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawnManager : MonoBehaviour {

    public GameObject zombie;
    public GameObject zombieBrute;
    public Transform spawnpoint;
    public float spawnRate;

    public int zombieHitPoints = 1;
    public float zombieMoveSpeed = 1.5f;
    public float zombieDamage = 0.1f;

    private void OnEnable() {
        GameManager.MinuteSurvived += HandleTimeEvent;
    }

    private void OnDisable() {
        GameManager.MinuteSurvived -= HandleTimeEvent;
    }

    private void HandleTimeEvent(float time) {

    }

    void Start() {
        StartSpawning();
    }

    private void StartSpawning() {
        InvokeRepeating("SpawnZombie", spawnRate, spawnRate);
    }
    
    private void SpawnZombie() {
        Vector3 spawnPoint = GetRandomSpawnPoint();


        GameObject prefabToSpawn = Random.value > 0.9 ? zombieBrute : zombie; // 10% chance to spawn a brute
        GameObject zombieInstance = Instantiate(prefabToSpawn, spawnPoint, Quaternion.identity);
        ZombieAI zombieAI = zombieInstance.GetComponent<ZombieAI>();

        int health = CalculateHealthBasedOnTime();
        float speed = CalculateSpeedBasedOnTime();
        zombieAI.Initialize(health, speed, zombieDamage);
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

    private int CalculateHealthBasedOnTime() {
        int minutes = Mathf.FloorToInt(GameManager.Instance.TimeAlive / 60);
        return 1 + minutes;
    }

    private float CalculateSpeedBasedOnTime() {
        int minutes = Mathf.FloorToInt(GameManager.Instance.TimeAlive / 60);
        return 1.0f + minutes / 2;
    }
}
