using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour {
    private PlayerInputActions _playerInputActions;

    private void Awake() {
        _playerInputActions = new PlayerInputActions();
        _playerInputActions.Player.Enable();
    }
    

    // public Vector2 GetMovementDirection() {
    //     Vector2 inputVector = _playerInputActions.Player.Drive.ReadValue<Vector2>();
    //
    //     inputVector = inputVector.normalized;
    //     
    //     return inputVector;
    // }

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
