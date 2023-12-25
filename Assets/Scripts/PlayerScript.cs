using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {
    
    protected Joystick movementJoystick;
    protected Joystick weaponJoystick;

    public ProjectileBehaviour ProjectilePrefab;
    public Transform LaunchOffset;
    public float projectileSpeed;
    public float shootCooldown = 0.1f; // Time between shots
    private float lastShootTime;
    public float projectileLifeSpawn; // Limit the distance the projectile travels

    private Rigidbody2D rigidbody2D;

    public float movementSpeed = 10f;

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

            if (Time.time - lastShootTime >= shootCooldown){
                Shoot();
                lastShootTime = Time.time;
            }
        }
    }

    private void Shoot() {
        ProjectileBehaviour newProjectile = Instantiate(ProjectilePrefab, LaunchOffset.position, transform.rotation);
        
        // Get the rigidbody of the projectile
        Rigidbody2D projectileRigidbody = newProjectile.GetComponent<Rigidbody2D>();

        // Calculate the aim direction
        Vector2 aimDirection = new Vector2(weaponJoystick.Horizontal, weaponJoystick.Vertical).normalized;

        // Apply velocity to the projectile's rigidbody
        projectileRigidbody.velocity = aimDirection * projectileSpeed; // You need to set the value of projectileSpeed
    }
}