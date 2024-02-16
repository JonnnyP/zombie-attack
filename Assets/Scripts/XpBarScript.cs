using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class XpBar : MonoBehaviour {
    
    public PlayerScript player;    
    public Slider xpBar;

    private void Start() {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
        xpBar = GetComponent<Slider>();

        xpBar.maxValue = player.GetMaxXP;
        xpBar.value = player.GetCurrentXP;
    }

    public void SetXP(float xp) {
        xpBar.value = xp;
    }

    public void SetMaxXp(float xp) {
        xpBar.maxValue = xp;
    }
}
