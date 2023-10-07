using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour {
    
    // direction
    // if clockwise and horizontal
    
    void Start() {
        
    }

    void Update() {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        // get center to collision point vector
        // dot product of the finish line intended direction
        // if dot product is negative then don't register it
    }
}
