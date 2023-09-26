using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour {

    public int Index => _index;
    [SerializeField] private int _index;
    
    public void Initialize(int index) {
        _index = index;
    }
}

