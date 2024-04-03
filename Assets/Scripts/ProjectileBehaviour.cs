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

        if(collision.gameObject.CompareTag("enemy")) {
            CancelInvoke("DestroyObject");
            Destroy(gameObject);            
        }
    }

    private void DestroyObject() {
        Destroy(gameObject);
    }
}
