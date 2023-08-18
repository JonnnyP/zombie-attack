using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Joybutton : MonoBehaviour, IPointerUpHandler, IPointerDownHandler {

    [HideInInspector]
    protected bool Pressed;

    void Start() {
        
    }

    // Update is called once per frame
    public void OnPointerDown(PointerEventData eventData) {
        Pressed = true;
    }

    public void OnPointerUp (PointerEventData eventData) {
        Pressed = false;
    }
}
