using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class UpgradeUIManager : MonoBehaviour {
    
    public GameObject player;
    public GameObject upgradePanel;

    public Button upgradeButton1;
    public Button upgradeButton2;
    public Button upgradeButton3;

    public Text upgradeChoice1;
    public Text upgradeChoice2;
    public Text upgradeChoice3;

    public UnityEvent onUpgradeSelected = new UnityEvent();


    void Start() {
        upgradePanel.SetActive(false);
    }

    public void ShowUpgradeChoices() {
        GenerateUpgradeChoices();
        upgradePanel.SetActive(true);

        upgradeButton1.onClick.AddListener(() => UpgradeSelected(1));
        upgradeButton2.onClick.AddListener(() => UpgradeSelected(2));
        upgradeButton3.onClick.AddListener(() => UpgradeSelected(3));
    }

    public void HideUpgradeChoice() {

        upgradePanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void UpgradeSelected(int upgradeChoice) {

        onUpgradeSelected.Invoke();
    }


    private void GenerateUpgradeChoices() {

        upgradeChoice1.text = "Weapon Fire rate";
        upgradeChoice2.text = "Bullet Speed";
        upgradeChoice3.text = "Movement Speed";
    }
}
