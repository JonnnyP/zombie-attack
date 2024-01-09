using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour {
    public float Speed = 4.5f;

    private void Update() {
        
        transform.position += transform.forward * Time.deltaTime * Speed;

        Invoke("DestroyObject", 0.5f);
    }   

    private void OnCollisionEnter2D(Collision2D collision) {

        ZombieScript zombieScript = collision.gameObject.GetComponent<ZombieScript>();
        
        if (zombieScript != null) {
            zombieScript.SpawnXPPoint();
            zombieScript.DeleteZombie();
        }
    }

    private void DestroyObject() {
        Destroy(gameObject);
    }
}
