using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerScript : MonoBehaviour {
    
    public float movementSpeed;
    public Joystick movementJoystick;
    new private Rigidbody2D rigidbody2D;

    public float playfieldSize = 20f;
    public float playfieldWidth = 20f;
    public float playfieldHeight = 20f;

    void Start() {
        
        rigidbody2D = GetComponent<Rigidbody2D>();
        GetComponent<Rigidbody2D>().gravityScale = 0f;

        movementJoystick = GameObject.FindGameObjectWithTag("movement-joystick").GetComponent<Joystick>();
    } 

    void Update() {
        HandleMovementInput();

        TeleportOnBoundaryCross();
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


    void TeleportOnBoundaryCross() {
        // Get the player's current position
        Vector3 playerPosition = transform.position;

        // Check if the player is beyond the X boundaries
        if (Mathf.Abs(playerPosition.x) > playfieldSize / 2f)
        {
            // Teleport to the opposite side
            playerPosition.x = -Mathf.Sign(playerPosition.x) * playfieldSize / 2f;
        }

        // Check if the player is beyond the Y boundaries
        if (Mathf.Abs(playerPosition.y) > playfieldSize / 2f)
        {
            // Teleport to the opposite side
            playerPosition.y = -Mathf.Sign(playerPosition.y) * playfieldSize / 2f;
        }

        // Update the player's position
        transform.position = playerPosition;
    }


    void CheckIfPlayerInBounds() {
        Vector2 playerPosition = transform.position;

        // Check if the player has moved beyond the playfield boundaries
        if (Mathf.Abs(playerPosition.x) > playfieldWidth / 2f) {
            playerPosition.x = -Mathf.Sign(playerPosition.x) * playfieldWidth / 2f;
        }

        if (Mathf.Abs(playerPosition.y) > playfieldHeight / 2f) {
            playerPosition.y = -Mathf.Sign(playerPosition.y) * playfieldHeight / 2f;
        }

        // Update the player's position
        transform.position = playerPosition;
    }

    public void IncreaseMovementSpeed() {
        movementSpeed += 0.3f;
    }

    public void DecreaseMovementSpeed() {
        movementSpeed -= 0.3f;
    }
}
