using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class PlayerScript : MonoBehaviour {
    
    public PlayerControllerScript playerControllerScript;
    public Joystick weaponJoystick;
    public ProjectileBehaviour ProjectilePrefab;
    private AudioManager audioManager;
    public UpgradeUIManager upgradeUIManager;

    public Transform LaunchOffset;
    public float projectileSpeed;
    public float shootCooldown; 
    private float lastShootTime;

    public HealthBar healthBar;
    private float maxHP = 50f;
    private float currentHP = 50f;

    public XpBar xpBar;
    public Text playerLevelText;
    private float currentXP = 0f;
    private int currentPlayerLevel = 1;
    private int nextLevelThreshold = 5;

    public delegate void PlayerLevelUp();
    public event PlayerLevelUp LevelUp;

    public UnityEvent onUpgradeSelected;

    private void Start() {

        weaponJoystick = GameObject.FindGameObjectWithTag("weapon-joystick").GetComponent<Joystick>();
        audioManager = FindObjectOfType<AudioManager>();
        
        ResetPlayerValues();
        upgradeUIManager.onUpgradeSelected.AddListener(HandleUpgradeSelection);
    }

    private void ResetPlayerValues() {
        currentHP = maxHP;
        currentXP = 0f;
        currentPlayerLevel = 1;
        nextLevelThreshold = 5;
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

    public int GetMaxXP {
        get { return nextLevelThreshold; }
    }

    private void AddXP(float xpAmount) {
        currentXP += xpAmount;
        xpBar.SetXP(currentXP);
        
        if(currentXP >= nextLevelThreshold) {
            TriggerLevelUp();
        }    
    }

    private void TriggerLevelUp() {
        LevelUp?.Invoke();
        
        currentPlayerLevel += 1;
        currentXP = 0;
        nextLevelThreshold += 2;

        xpBar.SetXP(currentXP);
        xpBar.SetMaxXp(nextLevelThreshold);
        playerLevelText.text = "Level " + currentPlayerLevel;

        if(currentPlayerLevel % 5 == 0) {
            Debug.Log("Increasing Difficulty");
        }

    }

    void HandleUpgradeSelection(int upgradeChoice) {
        switch(upgradeChoice) {
            case 1:
                UpgradeAttackSpeed();
                break;
            case 2:
                UpgradeProjectileSpeed();
                break;
            case 3:
                playerControllerScript.IncreaseMovementSpeed();
                break;
        }
    }

    private void UpgradeAttackSpeed() {
        if(shootCooldown >= 0) {
            shootCooldown -= 0.3f;
            shootCooldown = Mathf.Ceil(shootCooldown * 100) / 100;    
        }
    }

    private void UpgradeProjectileSpeed() {
        projectileSpeed += 0.75f;
    }
}
