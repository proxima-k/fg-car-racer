using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointChecker : MonoBehaviour {
    [SerializeField] private Rigidbody2D _rigidbody2D;
    private Vector2 test = Vector2.zero;
    
    private void Start() {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("Inside something");
        Debug.Log(other.gameObject);
    }
}



// has a list of cars
// function that process car positions
// store a list that has the cars' position in order
// has a event that send to the UI to tell it to update the order
//
// Things I would need:
// A Leaderboard UI script that handles the leading
//
// an event that contains information about cars position
//     with this, individual cars can handle their own UI
//
// the event would be invoked every time a checkpoint is passed
//     should be careful with the event being invoked multiple times in one frame.
//     maybe use a queue for updating to make it safe
//     or look up if events invocation could happen simultaneously
//
// I need something to identify the cars
//     a script that is called car?
//     perhaps the car script contains stats about the cars?
//     i suppose it makes sense to have a car controller as identifier since bots and humans need to control the car
//     with the control script

