using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CheckpointTest : MonoBehaviour {
    private void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("some objects has entered this collider");
        Debug.Log(other.gameObject);
    }
    
    
}
