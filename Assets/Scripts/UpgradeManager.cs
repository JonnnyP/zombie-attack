using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour {
    
    public static UpgradeManager Instance;

    public UpgradeUIManager upgradeUIManager;
    public PlayerScript playerScript;

    private void Awake() {
  
        if(Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
            return;
        }

        playerScript = GameObject.FindGameObjectWithTag("Player")?.GetComponent<PlayerScript>();
        upgradeUIManager.onUpgradeSelected.AddListener(ResumeGameplay);
    }

    void OnEnable() {

        playerScript.LevelUp += ShowUpgradeChoices;
    }

    void OnDisable() {
        playerScript.LevelUp -= ShowUpgradeChoices;
    }

    public void ShowUpgradeChoices() {
        if (upgradeUIManager != null) {
            Time.timeScale = 0f;
            upgradeUIManager.ShowUpgradeChoices();
        }
    }

    void ResumeGameplay() {
        upgradeUIManager.HideUpgradeChoice();
    }
}
