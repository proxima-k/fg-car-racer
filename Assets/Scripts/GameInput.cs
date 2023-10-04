using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour {
    private PlayerInputActions _playerInputActions;
    
    private void Awake() {
        _playerInputActions = new PlayerInputActions();
        _playerInputActions.General.Enable();
        
        _playerInputActions.General.PauseGame.performed += Player_OnPauseGame;
    }
    
    private void Player_OnPauseGame(InputAction.CallbackContext obj) {
        GameManager.Instance.TogglePauseGame();
    }
    
    private void OnEnable() {
        _playerInputActions.General.Enable();
    }

    private void OnDisable() {
        _playerInputActions.General.Disable();
    }

    private void OnDestroy() {
        _playerInputActions.General.PauseGame.performed -= Player_OnPauseGame;
        _playerInputActions.General.Disable();
    }
}
