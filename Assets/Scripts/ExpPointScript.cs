using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpPointScript : MonoBehaviour {
    
    public int xpValue = 10;

    public int XPValue {
        get {return xpValue; }
    }

    public void DeleteXpPoint() {

        Destroy(gameObject);
    }

    // private void OnCollisionEnter2D(Collision2D collision) {

    //     if(collision.gameObject.tag == "Player") {
    //         Destroy(gameObject);
    //     }
    // }
}
