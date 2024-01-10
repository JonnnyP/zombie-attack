using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieScript : MonoBehaviour {
    
    public Transform playerTransform;
    public float zombieMoveSpeed = 1.5f;
    private Rigidbody2D rb;
    public GameObject expPoint;

    private AudioManager audioManager;


    void Start() {

        audioManager = FindObjectOfType<AudioManager>();
        rb = GetComponent<Rigidbody2D>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update() {
        Vector2 direction = (playerTransform.position - transform.position).normalized;
        rb.velocity = direction * zombieMoveSpeed;
    }

    public void SpawnXPPoint() {
        
        Instantiate(expPoint, transform.position, Quaternion.identity);
    }

    public void DeleteZombie() {

        audioManager.PlayZombieDeathSound();
        Destroy(gameObject);
    }
}
