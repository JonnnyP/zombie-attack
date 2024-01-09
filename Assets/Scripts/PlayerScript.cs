using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {
    
    protected Joystick movementJoystick;
    protected Joystick weaponJoystick;

    // Projectile variables
    public ProjectileBehaviour ProjectilePrefab;
    public Transform LaunchOffset;
    public float projectileSpeed;
    public float shootCooldown; // Time between shots
    private float lastShootTime;
    public float projectileLifeSpawn; // Limit the distance the projectile travels
    public AudioClip shootSound;

    private Rigidbody2D rigidbody2D;

    public float movementSpeed;

    private int totalXP = 0;
    private int totalHP = 5;

    private void Start() {

        rigidbody2D = GetComponent<Rigidbody2D>();

        movementJoystick = GameObject.FindGameObjectWithTag("movement-joystick").GetComponent<Joystick>();
        weaponJoystick = GameObject.FindGameObjectWithTag("weapon-joystick").GetComponent<Joystick>();

        rigidbody2D.gravityScale = 0f;
    }

    private void Update() {
        // Handles movement 
        float horizontalMovement = movementJoystick.Horizontal * movementSpeed;
        float verticalMovement = movementJoystick.Vertical * movementSpeed;

        Vector2 newPosition = rigidbody2D.position + new Vector2(horizontalMovement, verticalMovement) * Time.deltaTime;
        rigidbody2D.MovePosition(newPosition);
    
        // Handles weapon
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

        AudioSource.PlayClipAtPoint(shootSound, transform.position);

        // Apply velocity to the projectile's rigidbody
        projectileRigidbody.velocity = aimDirection * projectileSpeed; // You need to set the value of projectileSpeed
    }

    private void OnCollisionEnter2D(Collision2D collision) {

        Rigidbody2D otherRigidBody = collision.gameObject.GetComponent<Rigidbody2D>();

        if (collision.gameObject.CompareTag("xp-point")) {
        
            HandleXPPointCollision(collision);

        } else if (collision.gameObject.CompareTag("enemy")) {

            // HandleEnemyCollision(collision);
        }
    }


    private void HandleXPPointCollision(Collision2D xpCollision) {


        ExpPointScript expPointScript = xpCollision.gameObject.GetComponent<ExpPointScript>();

        if( expPointScript != null) {

            AddXP(expPointScript.XPValue);
            expPointScript.DeleteXpPoint();
        }
    }

    public int TotalXP {

        get { return totalXP; }
    }

    public void AddXP(int xpAmount) {
        
        totalXP += xpAmount;

        Debug.Log("Player XP increased. Current XP: " + totalXP);
     
        // Update UI
    }

    private void HandleEnemyCollision(Collision2D enemyCollision) {

        ZombieScript zombieScript = enemyCollision.gameObject.GetComponent<ZombieScript>();
        
        if (zombieScript != null) {

        }
    }

    public int TotalHP {

        get { return totalHP; }
    }

    public void SubtractHP(int damage) {
        totalHP -= damage;
        Debug.Log("Player health decreased. Current HP: " + totalHP);
    }
}