using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PausePlayButton : MonoBehaviour {
    
    public Sprite playSprite;
    public Sprite pauseSprite;
    public Button button;

    private bool isPaused = false;

    void Start() {
        if (button != null) {
            button.onClick.AddListener(TogglePausePlay);
            UpdateButtonSprite();
        } else {
            Debug.LogError("Button reference is null. Make sure the 'button' field is assigned in the inspector.");
        }
    }

    private void TogglePausePlay() {
        isPaused = !isPaused;

        UpdateButtonSprite();

        Time.timeScale = isPaused ? 0f : 1f;
    }

    private void UpdateButtonSprite() {

        if (button != null && button.image != null) {
            button.image.sprite = isPaused ? playSprite : pauseSprite;
        } else {
            // Log an error message to help identify the issue
            Debug.LogError("Button or button's image reference is null. Make sure the 'button' field is assigned in the inspector and the button has an Image component.");
        }

    }

}
