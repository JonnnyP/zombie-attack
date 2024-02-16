using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour {
    
    public static UpgradeManager Instance;
    
    public UpgradeUIManager upgradeUIManager;
    private PlayerScript playerScript;

    private void Awake() {
  
        if(Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
            return;
        }

        // upgradeUIManager = FindObjectOfType<UpgradeUIManager>();
        playerScript = FindObjectOfType<PlayerScript>();

        if (playerScript == null) {
            Debug.LogError("PlayerScript not found!");
        }
        // playerScript = GameObject.FindGameObjectWithTag("Player")?.GetComponent<PlayerScript>();
        
        // upgradeUIManager = GameObject.FindGameObjectWithTag("upgrade-ui-manager")?.GetComponent<UpgradeUIManager>();
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

    void ResumeGameplay(int upgradeChoice) {
        upgradeUIManager.HideUpgradeChoice();
        Time.timeScale = 1f;
    }
}
