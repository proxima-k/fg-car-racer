using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour {
    private PlayerInputActions _playerInputActions;

    private void Awake() {
        _playerInputActions = new PlayerInputActions();
        _playerInputActions.Player.Enable();
        
        _playerInputActions.Player.PauseGame.performed += Player_OnPauseGame;
    }

    private void Player_OnPauseGame(InputAction.CallbackContext obj) {
        GameManager.Instance.TogglePauseGame();
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

    private void OnDestroy() {
        _playerInputActions.Player.Disable();
    }
}
