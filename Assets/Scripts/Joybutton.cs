using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour, IPointerUpHandler, IPointerDownHandler {

    [HideInInspector]
    protected bool Pressed;
    
    public ProjectileBehaviour ProjectilePrefab;
    public Transform LaunchOffset;

    private void Update() {
    }

    // public void OnPointerDown(PointerEventData eventData) {
    //     Pressed = true;

    //     Instantiate(ProjectilePrefab, LaunchOffset.position, transform.rotation);
    // }

    // public void OnPointerUp (PointerEventData eventData) {
    //     Pressed = false;
    // }
}
