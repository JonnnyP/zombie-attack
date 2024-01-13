using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpPointScript : MonoBehaviour {
    
    private AudioManager audioManager;
    public int xpValue;

    void Start() {
        audioManager = FindObjectOfType<AudioManager>();
    }

    public int GetXPValue {
        get {return xpValue; }
    }

    public void DeleteXpPoint() {

        audioManager.PlayXpPickUpSound();
        Destroy(gameObject);
    }

}
