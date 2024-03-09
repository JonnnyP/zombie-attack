using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerScript : MonoBehaviour {
    
    public float movementSpeed;
    public Joystick movementJoystick;
    new private Rigidbody2D rigidbody2D;

    // public float gridSize = 10f;
    public float playfieldSize = 20f; // Size of the playfield


    void Start() {
        
        rigidbody2D = GetComponent<Rigidbody2D>();
        GetComponent<Rigidbody2D>().gravityScale = 0f;

        movementJoystick = GameObject.FindGameObjectWithTag("movement-joystick").GetComponent<Joystick>();
    } 

    void Update() {
        HandleMovementInput();

        WrapAroundGrid();
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

    void WrapAroundGrid() {
        // Get the player's current position
        Vector2 playerPosition = transform.position;

        // Calculate the wrapped position based on the center of the map and playfield size
        float wrappedX = Mathf.Repeat(playerPosition.x + playfieldSize / 2f, playfieldSize) - playfieldSize / 2f;
        float wrappedY = Mathf.Repeat(playerPosition.y + playfieldSize / 2f, playfieldSize) - playfieldSize / 2f;

        // Set the wrapped position
        transform.position = new Vector2(wrappedX, wrappedY);
    }

    public void IncreaseMovementSpeed() {
        movementSpeed += 0.3f;
    }

    public void DecreaseMovementSpeed() {
        movementSpeed -= 0.3f;
    }
}
