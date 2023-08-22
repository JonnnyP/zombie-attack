using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {
    
    protected Joystick movementJoystick;
    protected Joystick weaponJoystick;

    public ProjectileBehaviour ProjectilePrefab;
    public Transform LaunchOffset;

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
    
        float aimX = weaponJoystick.Horizontal;
        float aimY = weaponJoystick.Vertical;

        // Calculate aim direction angle
        Vector2 aimDirection = new Vector2(aimX, aimY);
        if (aimDirection.magnitude > 0.1f) {
            // Calculate the angle and adjust for the left-handed coordinate system
            float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
            aimAngle -= 90f; // Adjust for sprite orientation

            // Apply the rotation
            transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, aimAngle));

            // Instantiate(ProjectilePrefab, LaunchOffset.position, transform.rotation);

        }
    }
}