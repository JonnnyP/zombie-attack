using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    
    // private static GameManager instance;
    public PlayerScript playerScript;
    public Text timeText;

    private float startTime;
    public static GameManager Instance { get; private set; }

    public float TimeAlive { get; private set; }

    public delegate void TimerEvent(float time);
    public static event TimerEvent MinuteSurvived;

    private void Awake() {

        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
            return;
        }

        // Callback to run the OnSceneLoaded method after a new scene loads
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode) {

        if( scene.name == "GamePlayScene") {
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

                if(TimeAlive % 60 == 0) {
                    TriggerTimeEvent(TimeAlive);
                }

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

    public static void TriggerTimeEvent(float time) {
        MinuteSurvived?.Invoke(time);
        Debug.Log("Time event, survived 1 minute");
    }    
}
