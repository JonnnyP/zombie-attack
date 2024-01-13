using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {
    
    public Joystick weaponJoystick;
    public ProjectileBehaviour ProjectilePrefab;
    private AudioManager audioManager;

    public Transform LaunchOffset;
    public float projectileSpeed;
    public float shootCooldown; 
    private float lastShootTime;

    public HealthBar healthBar;
    private float maxHP = 50f;
    private float currentHP = 50f;

    private float currentXP = 0f;
    private int currentPlayerLevel = 1;
    private int nextLevelThreshold = 5;

    private void Start() {

        weaponJoystick = GameObject.FindGameObjectWithTag("weapon-joystick").GetComponent<Joystick>();
        audioManager = FindObjectOfType<AudioManager>();
        currentHP = maxHP;
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

        if (collision.gameObject.CompareTag("xp-point")) {
        
            HandleXPPointCollision(collision);
        }
    }

    private void OnCollisionStay2D(Collision2D collision) {

        if (collision.gameObject.CompareTag("enemy")) {

            HandleEnemyCollision(collision);
        }    
    }

    private void HandleXPPointCollision(Collision2D xpCollision) {


        ExpPointScript expPointScript = xpCollision.gameObject.GetComponent<ExpPointScript>();

        if( expPointScript != null) {

            AddXP(expPointScript.GetXPValue);
            expPointScript.DeleteXpPoint();
        }
    }

    private void HandleEnemyCollision(Collision2D enemyCollision) {

        ZombieAI zombieAI = enemyCollision.gameObject.GetComponent<ZombieAI>();
        
        if (zombieAI != null) {
            
            DamagePlayer(zombieAI.GetDamage);
        }
    }

    public float GetCurrentHP {

        get { return currentHP; }
    }

    public float MaxHP {
        get { return maxHP; }
    }

    public void HealPlayer(float healAmount) {
        currentHP += healAmount;
    }

    public void DamagePlayer(float damage) {
        currentHP -= damage;
        healthBar.SetHealth(currentHP);
    }
    
    public float GetCurrentXP {

        get { return currentXP; }
    }

    private void AddXP(float xpAmount) {
        
        currentXP += xpAmount;
        CheckLevelUp();
    }

    public void CheckLevelUp() {
        
        if((currentXP % nextLevelThreshold) == 0) {
            currentPlayerLevel += 1;

            // Upgrade a single attribute 
            // i.e. movement speed, boolet speed, boolet rate of fire

            Debug.Log("Level Up! Curent level: " + currentPlayerLevel);
        }    
    }
}
