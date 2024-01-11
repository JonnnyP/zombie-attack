using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAI : MonoBehaviour {
    
    public Transform playerTransform;
    private Rigidbody2D rb;

    public float zombieMoveSpeed;
    public float zombieDamage;

    public GameObject expPoint;
    private AudioManager audioManager;


    void Start() {;

        audioManager = FindObjectOfType<AudioManager>();
        rb = GetComponent<Rigidbody2D>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update() {
        Vector2 direction = (playerTransform.position - transform.position).normalized;
        rb.velocity = direction * zombieMoveSpeed;
    }

    public float GetDamage {
        get{ return zombieDamage; }
    }

    public void SpawnXPPoint() {
        
        Instantiate(expPoint, transform.position, Quaternion.identity);
    }

    public void DeleteZombie() {

        audioManager.PlayZombieDeathSound();
        Destroy(gameObject);
    }
}
