using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerScript : MonoBehaviour {
    
    public float movementSpeed;
    public Joystick movementJoystick;
    new private Rigidbody2D rigidbody2D;

    void Start() {
        
        rigidbody2D = GetComponent<Rigidbody2D>();
        GetComponent<Rigidbody2D>().gravityScale = 0f;

        movementJoystick = GameObject.FindGameObjectWithTag("movement-joystick").GetComponent<Joystick>();
    } 

    void Update() {
        HandleMovementInput();
    }

    void HandleMovementInput() {

        if(movementJoystick != null) {

            float horizontalMovement = movementJoystick.Horizontal * movementSpeed;
            float verticalMovement = movementJoystick.Vertical * movementSpeed;

            Vector2 newPosition = rigidbody2D.position + new Vector2(horizontalMovement, verticalMovement) * Time.deltaTime;
            rigidbody2D.MovePosition(newPosition);

        } else {
            Debug.LogError("Movement joystick not found");
        } 
    }

}
