using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieScript : MonoBehaviour {
    
    public Transform playerTransform;
    public float zombieMoveSpeed = 1.5f;
    private Rigidbody2D rb;
    public GameObject expPoint;

    void Start() {
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

        Destroy(gameObject);
    }

    // private void OnCollisionEnter2D(Collision2D collision) {

    //     if(collision.gameObject.tag == "projectile") {
    //         Destroy(gameObject);

    //         Instantiate(expPoint, transform.position, Quaternion.identity);

    //     }
    // }
}
