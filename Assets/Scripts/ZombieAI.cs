using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieScript : MonoBehaviour {
    
    public Transform playerTransform;
    public float zombieMoveSpeed = 1.5f;
    private Rigidbody2D rb;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update() {
        Vector2 direction = (playerTransform.position - transform.position).normalized;
        rb.velocity = direction * zombieMoveSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision) {

        if(collision.gameObject.tag == "projectile") {
            Destroy(gameObject);
        }
    }
}
