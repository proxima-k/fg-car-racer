using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckpointChecker : MonoBehaviour {

    [SerializeField] private int _currentProgressIndex;
    [SerializeField] private int _lastIndex;

    [SerializeField] private int _currentLap = 0;
    
    // event for wrong way
    // this is for the UI or Debugger to know that the car is going in the wrong direction
    public event EventHandler OnGoingWrongPath;
    
    // event for right way (which notifies the rank manager)
    // public event EventHandler
    public event EventHandler OnGoingRightPath;
    public event EventHandler OnStartingNewLap;
    
    private void Start() {
        // get the last index of the checkpoints from a manager script
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.TryGetComponent(out Checkpoint checkpoint)) {
            int collidedCheckpointIndex = checkpoint.Index;
            
            if (_currentProgressIndex == collidedCheckpointIndex)
                return;
            
            // going the wrong way
            
            
            /* going the right way means
                 if current progress index is larger by 1
                 if current progress index is at last index and collided index is at 0
            */
            if (collidedCheckpointIndex < _currentProgressIndex && !hasPassedLastCheckpoint()) {
                Debug.Log("Wrong checkpoint!");
                OnGoingWrongPath?.Invoke(this, EventArgs.Empty);
                return;
            }
            
            if (_currentProgressIndex + 1 == collidedCheckpointIndex) {
                _currentProgressIndex = collidedCheckpointIndex;
                OnGoingRightPath?.Invoke(this, EventArgs.Empty);
                Debug.Log($"Correct checkpoint! {collidedCheckpointIndex}");
            } else if (collidedCheckpointIndex == 0 && hasPassedLastCheckpoint()) {
                _currentProgressIndex = collidedCheckpointIndex;
                _currentLap++;
                OnStartingNewLap?.Invoke(this, EventArgs.Empty);
                Debug.Log($"Correct checkpoint! {collidedCheckpointIndex}");
                Debug.Log($"New Lap! {_currentLap}");
            }
            
        }
    }

    private bool hasPassedLastCheckpoint() {
        return _currentProgressIndex == _lastIndex;
    }
}
