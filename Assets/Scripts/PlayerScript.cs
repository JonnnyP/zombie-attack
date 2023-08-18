using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {
    
    protected Joystick joystick;
    protected Joybutton joybutton;

    private Rigidbody2D rigidbody2D;

    public float movementSpeed = 300f;

    private void Start() {

        joystick = FindObjectOfType<Joystick>();
        joybutton = FindObjectOfType<Joybutton>();
        rigidbody2D = GetComponent<Rigidbody2D>();

        rigidbody2D.gravityScale = 0f;
    }

    private void Update() {

        float horizontalMovement = joystick.Horizontal * movementSpeed;
        float verticalMovement = joystick.Vertical * movementSpeed;


        Vector2 newPosition = rigidbody2D.position + new Vector2(horizontalMovement, verticalMovement) * Time.deltaTime;
        rigidbody2D.MovePosition(newPosition);
    }
}