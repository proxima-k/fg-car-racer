using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour {
    private PlayerInputActions _playerInputActions;
    
    private void Awake() {
        _playerInputActions = new PlayerInputActions();
        _playerInputActions.General.Enable();
        
        _playerInputActions.General.PauseGame.performed += Player_OnPauseGame;
        
        GameManager.Instance.OnGameEnd += GameManager_OnGameEnd;
        GameManager.Instance.OnGameRestart += GameManager_OnGameRestart;
    }
    

    private void GameManager_OnGameRestart(object sender, EventArgs e) {
        _playerInputActions.General.Enable();
    }

    private void GameManager_OnGameEnd(object sender, GameManager.OnGameEndEventArgs e) {
        _playerInputActions.General.Disable();
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
}
