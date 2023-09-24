using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour {
    private Rigidbody2D _carRigidBody2D;
    [SerializeField] private GameInput _gameInput;

    [SerializeField] private float _maxSpeed = 50f;
    [SerializeField] private float _accelerateStrength = 5f;
    [SerializeField] private float _steerStrength = 3f;
    [SerializeField] [Range(0.1f, 1f)] private float _driftStrength = 0.92f;

    private float _accelerateInput;
    private float _steerInput;
    
    
    private void Start() {
        _carRigidBody2D = GetComponent<Rigidbody2D>();
        // _gameInput.
    }

    private void Update() {
        _accelerateInput = _gameInput.GetAccelerateInput();
        _steerInput = _gameInput.GetSteerInput();
    }

    void FixedUpdate() {

        Accelerate();
        Drift();
        
        Steer();
        Drag();
    }

    
    private void Accelerate() {

        if (_carRigidBody2D.velocity.magnitude > _maxSpeed)
            return;
        
        Vector2 accelerateForce = transform.up * (_accelerateStrength * _accelerateInput);
        
        _carRigidBody2D.AddForce(accelerateForce, ForceMode2D.Force);
    }

    private void Steer() {
        // to prevent rotating the car when the car isn't moving
        float steerMagnitude = 7f;
        float velocityToRotationRatio = _carRigidBody2D.velocity.magnitude / steerMagnitude;
        
        float currentAngle = transform.eulerAngles.z;
        float rotateAmount = (_steerStrength * _steerInput * velocityToRotationRatio);

        // to make reversing the car more intuitive (press steer left key will make the car steer to the left)
        if (_accelerateInput < 0)
            rotateAmount *= -1;
        
        float newAngle = currentAngle - rotateAmount;
        
        _carRigidBody2D.MoveRotation(newAngle);
    }

    
    // vertical friction to slow down the car
    private void Drag() {
        float maxDrag = 2f;
        
        if (_accelerateInput == 0) {
            _carRigidBody2D.drag = Mathf.Lerp(_carRigidBody2D.drag, maxDrag, Time.deltaTime * 3);
            return;
        }

        _carRigidBody2D.drag = 0;
    }
    
    // horizontal friction to prevent car from slipping sideways
    private void Drift() {
        // the velocity of the car's y-axis (forward and backwards)
        Vector2 forwardVelocity = transform.up * Vector2.Dot(transform.up, _carRigidBody2D.velocity);
        
        // the velocity of the car's x-axis (where the car doors are facing)
        Vector2 sideVelocity = transform.right * Vector2.Dot(transform.right, _carRigidBody2D.velocity);

        _carRigidBody2D.velocity = forwardVelocity + (sideVelocity * _driftStrength);
    }
}