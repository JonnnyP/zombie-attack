using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAI : MonoBehaviour {
    
    public Transform playerTransform;
    private Rigidbody2D rb;

    public int hitPoints;
    public float zombieMoveSpeed;
    public float zombieDamage;

    public GameObject expPoint;
    private AudioManager audioManager;

    // For pivot point to point at player
    public GameObject visualObject;

    void Start() {;

        audioManager = FindObjectOfType<AudioManager>();
        rb = GetComponent<Rigidbody2D>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update() {
        Vector3 direction = (playerTransform.position - transform.position).normalized;
        rb.velocity = direction * zombieMoveSpeed;

        // Calculate the angle in degrees
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Apply the rotation directly to the visual part of the zombie
        // rb.transform.rotation = Quaternion.Euler(0, 0, angle);
        rb.rotation = angle;
    }

    public void Initialize(int hp, float moveSpeed, float damage) {
        this.hitPoints = hp;
        this.zombieMoveSpeed = moveSpeed;
        this.zombieDamage = damage;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.tag == "projectile") {
            hitPoints--;

            if(hitPoints <= 0) {
                audioManager.PlayZombieDeathSound();
                Destroy(gameObject);
                SpawnXPPoint();
            }
        }
    }

    public float GetDamage {
        get{ return zombieDamage; }
    }

    public void SpawnXPPoint() {
        
        Instantiate(expPoint, transform.position, Quaternion.identity);
    }

    public void DeleteZombie() {

        // audioManager.PlayZombieDeathSound();
        Destroy(gameObject);
    }
}
