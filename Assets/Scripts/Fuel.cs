using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fuel : MonoBehaviour {

    // spawn every few seconds?
    [SerializeField] private int fillAmount = 25;
    [SerializeField] private GameObject _body;

    [SerializeField] private float _reappearTime = 5f;
    private float _timer = 0;
    private bool _onCooldown = false;
    
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
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.transform.root.TryGetComponent(out CarController carController)) {
            carController.AddFuel(fillAmount);
            Hide();
        }
    }

    private void Show() {
        _body.SetActive(true);
    }

    private void Hide() {
        _body.SetActive(false);
        _timer = _reappearTime;
        _onCooldown = true;
    }
}
