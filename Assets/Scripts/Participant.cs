using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Participant : MonoBehaviour {

    public string Name;

    public int Index => _index;
    public bool IsCheating => _isCheating;
    public int LapsCompleted => _lapsCompleted < 0 ? 0 : _lapsCompleted;
    
    private int _index;
    private bool _isCheating = false;
    private int _lapsCompleted = -1; // since they have to pass the finish line initially 

    private Vector3 _startingPosition;
    private Quaternion _startingQuaternion;
    
    public void Reset() {
        // resets fuel
        CarController carController = GetComponent<CarController>();
        carController.Reset();

        // resets position
        transform.position = _startingPosition;
        transform.rotation = _startingQuaternion;

        _lapsCompleted = -1;
    }

    public void Initialize(Vector3 startingPosition, Quaternion startingQuaternion) {
        _startingPosition = startingPosition;
        _startingQuaternion = startingQuaternion;
        
        Reset();
    }
    
    public void SetCheating(bool isCheating) {
        _isCheating = isCheating;
    }

    public void AddLapCompleted() {
        _lapsCompleted++;
        Logger.Log($"Lap Completed: {_lapsCompleted}");
    }
}
