using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    
    private static GameManager instance;
    public PlayerScript playerScript;
    public Text timeText;

    private float startTime;
    public float TimeAlive { get; private set; }

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
            timeText = GameObject.FindGameObjectWithTag("Time-Display")?.GetComponent<Text>();
            startTime = Time.time;
        }
    }

    void Update() {

        if(playerScript != null) {

            if(playerScript.GetCurrentHP >= 0) {
                
                TimeAlive = Time.time - startTime;
                UpdateTimeDisplay(TimeAlive);

            } else if (playerScript.GetCurrentHP <= 0) {

                GameOver();
            }
        }
    }

    void GameOver() {
        SceneManager.LoadScene(0);
    }

    private void UpdateTimeDisplay(float newTime) {
        timeText.text = "" + newTime.ToString("F2") + ""; 
    }
}
