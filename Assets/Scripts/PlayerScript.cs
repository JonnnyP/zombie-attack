using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {
    
    protected Joystick movementJoystick;
    protected Joystick weaponJoystick;

    private Rigidbody2D rigidbody2D;

    public float movementSpeed = 300f;

    private void Start() {

        rigidbody2D = GetComponent<Rigidbody2D>();

        movementJoystick = GameObject.FindGameObjectWithTag("movement-joystick").GetComponent<Joystick>();
        weaponJoystick = GameObject.FindGameObjectWithTag("weapon-joystick").GetComponent<Joystick>();

        rigidbody2D.gravityScale = 0f;
    }

    private void Update() {

        float horizontalMovement = movementJoystick.Horizontal * movementSpeed;
        float verticalMovement = movementJoystick.Vertical * movementSpeed;

        Vector2 newPosition = rigidbody2D.position + new Vector2(horizontalMovement, verticalMovement) * Time.deltaTime;
        rigidbody2D.MovePosition(newPosition);
    }
}