using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour {
    public float Speed = 4.5f;

    private void Update() {
        
        // shoots up only
        transform.position += transform.up * Time.deltaTime * Speed;
        
        // projectiles stay in place
        // need to change joybutton to joystick so player can turn/aim
        // transform.position += transform.forward * Time.deltaTime * Speed;
    }   

    private void OnCollisionEnter2D(Collision2D collision) {
        Destroy(gameObject);
    }
}
