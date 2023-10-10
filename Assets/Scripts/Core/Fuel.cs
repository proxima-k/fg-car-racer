using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fuel : MonoBehaviour {
    
    [SerializeField] private FuelSO _fuelSO;
    
    [SerializeField] private GameObject _body;
    private float _timer = 0;
    private bool _onCooldown = false;

    private void Start() {
        _body.GetComponent<SpriteRenderer>().color = _fuelSO.SpriteColor;
    }

    private void Update() {
        if (_timer > 0) {
            _timer -= Time.deltaTime;
        }
        else {
            if (_onCooldown) {
                _onCooldown = false;
                Show();
            }
        }
        
        // rotate
    }

    private void OnValidate() {
        _body.GetComponent<SpriteRenderer>().color = _fuelSO.SpriteColor;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.transform.root.TryGetComponent(out CarController carController)) {
            carController.AddFuel(_fuelSO.FillAmount);
            Cooldown();
        }
    }

    private void Show() {
        _body.SetActive(true);
    }

    private void Hide() {
        _body.SetActive(false);
    }

    private void Cooldown() {
        Hide();
        _timer = _fuelSO.ReappearTime;
        _onCooldown = true;
    }
}
