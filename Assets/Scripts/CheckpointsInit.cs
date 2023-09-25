using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CheckpointsInit : MonoBehaviour {

    [SerializeField] private Transform _startingPointTf;
    [SerializeField] private Tilemap _raceTrackTilemap;
    
    // clockwise or anti-clockwise 
    
    private void Start() {
    }

    private void OnDrawGizmos() {
        // Debug.Log(_raceTrackTilemap.LocalToCell(_startingPointTf.localPosition));
    }
    
    // store starting cell
    // start processing through clockwise
}
