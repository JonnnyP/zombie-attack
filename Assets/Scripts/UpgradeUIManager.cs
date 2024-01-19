using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeUIManager : MonoBehaviour {
    
    public Text upgradeText;

    public void ShowUpgradeChoices() {

        upgradeText.text = "Choose an Upgrade!";
    }
}
