using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour {
    public float Speed;
    public float destroyDelay;

    private void Start() {
        Invoke("DestroyObject", destroyDelay);
    }

    private void OnCollisionEnter2D(Collision2D collision) {

<<<<<<< HEAD
        if(collision.gameObject.CompareTag("enemy")) {
            CancelInvoke("DestroyObject");
            Destroy(gameObject);            
        }
=======
        // ZombieAI zombieAI = collision.gameObject.GetComponent<ZombieAI>();

        // if(collision.gameObject.CompareTag("enemy")) {
        //     CancelInvoke("DestroyObject");
        //     Destroy(gameObject);            
        // }

        CancelInvoke("DestroyObject");
        Destroy(gameObject);
>>>>>>> a1a2757cb54926ce0f6875520066374c84566688
    }

    private void DestroyObject() {
        Destroy(gameObject);
    }
}
