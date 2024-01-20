using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    
    private static GameManager instance;
    public PlayerScript playerScript;

    private void Awake() {

        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
            return;
        }

        // Callback to run the OnSceneLoaded method after a new scene loads
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode) {

        if( scene.name == "MainScene") {
            playerScript = GameObject.FindGameObjectWithTag("Player")?.GetComponent<PlayerScript>();
        }
    }

    void Update() {
        if (playerScript != null && playerScript.GetCurrentHP <= 0) {
            GameOver();    
        }   
    }

    void GameOver() {
        SceneManager.LoadScene(0);
    }
}
