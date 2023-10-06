using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour {
    private PlayerInputActions _playerInputActions;
    
    private void Awake() {
        _playerInputActions = new PlayerInputActions();
        _playerInputActions.Player.Enable();
    }

    public void SetControlScheme(string schemeName) {
        _playerInputActions.bindingMask = InputBinding.MaskByGroup(schemeName);
    }
    
    public float GetAccelerateInput() {
        return _playerInputActions.Player.Accelerate.ReadValue<float>();
    }

    public float GetSteerInput() {
        return _playerInputActions.Player.Steer.ReadValue<float>();
    }

    public int GetNitroInput() {
        return _playerInputActions.Player.Boost.IsInProgress() ? 1 : 0;
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
