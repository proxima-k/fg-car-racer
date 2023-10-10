using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurboAnimator : MonoBehaviour {
    private SpriteRenderer _spriteRenderer;
    
    private void Awake() {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.enabled = false;
    }

    private void Start() {
        CarController carController = GetComponentInParent<CarController>();
        carController.OnUsingFuel += CarController_OnUsingFuel;
        carController.OnNotUsingFuel += CarController_OnNotUsingFuel;
    }

    private void CarController_OnNotUsingFuel(object sender, EventArgs e) {
        _spriteRenderer.enabled = false;
    }
    
    private void CarController_OnUsingFuel(object sender, EventArgs e) {
        _spriteRenderer.enabled = true;
    }
}
