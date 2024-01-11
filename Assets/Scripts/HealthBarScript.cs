using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {

    public Slider healthBar;
    public PlayerScript player;
    
    private void Start() {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
        healthBar = GetComponent<Slider>();

        healthBar.maxValue = player.MaxHP;
        healthBar.value = player.CurrentHP;
    }
    
    public void SetHealth(float hp) {
        healthBar.value = hp;
    }
}

