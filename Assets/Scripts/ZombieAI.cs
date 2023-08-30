using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieScript : MonoBehaviour {
    
    public Transform playerTransform;
    public float zombieMoveSpeed = 4f;
    private Rigidbody2D rb;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update() {
        Vector2 direction = (playerTransform.position - transform.position).normalized;
        rb.velocity = direction * zombieMoveSpeed;
    }
}
