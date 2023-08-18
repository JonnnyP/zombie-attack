using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustWallsForAspectRatio : MonoBehaviour {

    public Transform topWall;
    public Transform bottomWall;
    public Transform leftWall;
    public Transform rightWall;

    private void Start() {
    
        float cameraHeight = Camera.main.orthographicSize * 2;
        float cameraWidth = cameraHeight * Camera.main.aspect;

        // Adjust wall positions based on camera dimensions
        topWall.position = new Vector3(0f, cameraHeight / 2, 0f);
        bottomWall.position = new Vector3(0f, -cameraHeight / 2, 0f);
        leftWall.position = new Vector3(-cameraWidth / 2, 0f, 0f);
        rightWall.position = new Vector3(cameraWidth / 2, 0f, 0f);
    }
}
