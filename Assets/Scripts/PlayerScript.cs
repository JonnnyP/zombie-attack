using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {
    
    public Joystick weaponJoystick;
    public ProjectileBehaviour ProjectilePrefab;
    public Transform LaunchOffset;
    public float projectileSpeed;
    public float shootCooldown; // Time between shots
    private float lastShootTime;

    private AudioManager audioManager;

    private int totalXP = 0;
    private int totalHP = 5;

    private void Start() {

        weaponJoystick = GameObject.FindGameObjectWithTag("weapon-joystick").GetComponent<Joystick>();
        audioManager = FindObjectOfType<AudioManager>();
    }

    private void Update() {
    
        HandleWeaponInput();
    }

    void HandleWeaponInput() {

        if(weaponJoystick != null) {
            float aimX = weaponJoystick.Horizontal;
            float aimY = weaponJoystick.Vertical;

            // Calculate aim direction angle
            Vector2 aimDirection = new Vector2(aimX, aimY);
            
            if (aimDirection.magnitude > 0.1f) {

                // Calculate the angle and adjust for the left-handed coordinate system
                float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
                aimAngle -= 90f;

                // Apply the rotation
                transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, aimAngle));

                if (Time.time - lastShootTime >= shootCooldown){
                    Shoot();
                    lastShootTime = Time.time;  
                }

            }
        } else {
            Debug.LogError("Weapon joystick not found");
        }

    }

    private void Shoot() {
        ProjectileBehaviour newProjectile = Instantiate(ProjectilePrefab, LaunchOffset.position, transform.rotation);
        
        // Get the rigidbody of the projectile
        Rigidbody2D projectileRigidbody = newProjectile.GetComponent<Rigidbody2D>();

        // Calculate the aim direction
        Vector2 aimDirection = new Vector2(weaponJoystick.Horizontal, weaponJoystick.Vertical).normalized;

        audioManager.PlayProjectileSound();

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
            audioManager.PlayXpPickUpSound();
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