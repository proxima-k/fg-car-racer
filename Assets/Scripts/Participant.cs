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

    public void ToggleIsCheating() {
        _isCheating = !_isCheating;
    }

    public void AddLapCompleted() {
        _lapsCompleted++;
        // trigger onLapAdded
        Logger.Log($"Lap Completed: {_lapsCompleted}");
    }
}
