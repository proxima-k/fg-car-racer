using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour {

    public event EventHandler OnUsingFuel;
    public event EventHandler OnNotUsingFuel;
    public event EventHandler<OnFuelChangedEventArgs> OnFuelChanged;
    public class OnFuelChangedEventArgs : EventArgs {
        public float fuelNormalized;
    }
    
    private Rigidbody2D _carRigidBody2D;
    [SerializeField] private PlayerInput _playerInput;

    // Car Control Settings
    [SerializeField] private float _maxSpeed = 40f;
    [SerializeField] private float _accelerateStrength = 4f;
    [SerializeField] private float _steerStrength = 3f;
    [SerializeField] [Range(0.1f, 1f)] private float _driftStrength = 0.92f;

    // Boost Settings
    [SerializeField] private float _fuelBurnSpeed = 10f;
    [SerializeField] private float _maxFuel = 100;
    [SerializeField] private float _bonusSpeed = 10f;
    [SerializeField] private float _bonusAcceleration = 3f;
    private float _currentFuel;
    private float _startingFuel = 50f;

    private float _accelerateInput;
    private float _steerInput;
    private int _boostInput;

    private void Awake() {
        _currentFuel = _startingFuel;
        _carRigidBody2D = GetComponent<Rigidbody2D>();
        _playerInput = GetComponent<PlayerInput>();
    }

    private void Start() {
        
        OnFuelChanged?.Invoke(this, new OnFuelChangedEventArgs{ fuelNormalized = _currentFuel/_maxFuel});
    }

    private void Update() {
        _accelerateInput = _playerInput.GetAccelerateInput();
        _steerInput = _playerInput.GetSteerInput();
        _boostInput = _playerInput.GetNitroInput();
    }

    void FixedUpdate() {
        BurnFuel();
        Accelerate();
        Drift();
        
        Steer();
        Drag();
    }

    private void BurnFuel() {
        if (_boostInput == 0 || !HasFuel()) {
            OnNotUsingFuel?.Invoke(this, EventArgs.Empty);   
            return;
        }
        
        _currentFuel -= _boostInput * _fuelBurnSpeed * Time.deltaTime;
        
        if (_currentFuel <= 0) {
            _currentFuel = 0;
        }
 
        OnUsingFuel?.Invoke(this, EventArgs.Empty);
        OnFuelChanged?.Invoke(this, new OnFuelChangedEventArgs{fuelNormalized = _currentFuel / _maxFuel});
    }
    
    public void AddFuel(float value) {
        _currentFuel += value;
        if (_currentFuel > _maxFuel) {
            _currentFuel = _maxFuel;
        }
        OnFuelChanged?.Invoke(this, new OnFuelChangedEventArgs {fuelNormalized = _currentFuel / _maxFuel});
    }

    public void Reset() {
        _currentFuel = _startingFuel;
        _carRigidBody2D.velocity = Vector2.zero;
        OnFuelChanged?.Invoke(this, new OnFuelChangedEventArgs {fuelNormalized = _currentFuel / _maxFuel});
    }
    
    private bool HasFuel() {
        return _currentFuel > 0;
    }
    
    private void Accelerate() {
        float totalMaxSpeed = _maxSpeed;
        float totalAccelerateStrength = (_accelerateStrength * _accelerateInput);

        if (HasFuel()) {
            totalMaxSpeed += (_bonusSpeed * _boostInput);
            totalAccelerateStrength += (_bonusAcceleration * _boostInput);
        }
        
        if (_carRigidBody2D.velocity.magnitude > totalMaxSpeed)
            return;
        
        Vector2 accelerateForce = transform.up * totalAccelerateStrength;
        
        _carRigidBody2D.AddForce(accelerateForce, ForceMode2D.Force);
    }

    private void Steer() {
        // to prevent rotating the car when the car isn't moving
        float steerMagnitude = 7f;
        float velocityToSteerRatio = _carRigidBody2D.velocity.magnitude / steerMagnitude;
        
        float currentAngle = transform.eulerAngles.z;
        float rotateAmount = (_steerStrength * _steerInput * velocityToSteerRatio);

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