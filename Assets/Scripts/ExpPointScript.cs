using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpPointScript : MonoBehaviour {
    
    public int xpValue;

    public void CollectXP() {

        Destroy(gameObject);
    }
}
