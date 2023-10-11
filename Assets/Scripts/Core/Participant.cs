using System;
using UnityEngine;

public class Participant : MonoBehaviour {

    public string Name;

    public int Index => _index;
    public bool IsCheating => _isCheating;
    public int LapsCompleted => _lapsCompleted < 0 ? 0 : _lapsCompleted;

    public event EventHandler<OnLapCompleteEventArgs> OnLapCompleted;
    public class OnLapCompleteEventArgs : EventArgs {
        public int lapsCompleted;
    }
    
    private int _index;
    private bool _isCheating = false;
    private int _lapsCompleted = -1; // since they have to pass the finish line initially 

    private Vector3 _startingPosition;
    private Quaternion _startingQuaternion;
    
    public void Initialize(Vector3 startingPosition, Quaternion startingQuaternion) {
        _startingPosition = startingPosition;
        _startingQuaternion = startingQuaternion;
        GameManager.Instance.OnGameRestart += GameManager_OnGameRestart;

        Reset();
    }
    
    private void GameManager_OnGameRestart(object sender, EventArgs e) {
        Reset();
    }

    public void Reset() {
        // resets fuel
        CarController carController = GetComponent<CarController>();
        carController.Reset();

        // resets position
        transform.position = _startingPosition;
        transform.rotation = _startingQuaternion;

        _lapsCompleted = -1;
        OnLapCompleted?.Invoke(this, new OnLapCompleteEventArgs { lapsCompleted = LapsCompleted });
    }

    
    public void SetCheating(bool isCheating) {
        _isCheating = isCheating;
    }

    public void AddLapCompleted() {
        _lapsCompleted++;
        
        OnLapCompleted?.Invoke(this, new OnLapCompleteEventArgs { lapsCompleted = LapsCompleted });
        Utils.Log($"{Name} -- Laps Completed: {_lapsCompleted}");
    }
}
