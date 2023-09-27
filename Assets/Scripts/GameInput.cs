using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour {
    private PlayerInputActions _playerInputActions;

    private void Awake() {
        _playerInputActions = new PlayerInputActions();
        _playerInputActions.Player.Enable();
        
        // _playerInputActions.
    }

    public float GetAccelerateInput() {
        return _playerInputActions.Player.Accelerate.ReadValue<float>();
    }

    public float GetSteerInput() {
        return _playerInputActions.Player.Steer.ReadValue<float>();
    }
    
    
    private void OnEnable() {
        _playerInputActions.Player.Enable();
    }

    private void OnDisable() {
        _playerInputActions.Player.Disable();
    }
}
