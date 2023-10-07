using System;
using Unity.VisualScripting;
using UnityEngine;

public class FinishLine : MonoBehaviour {

    private Transform _bodyTransform;

    private void Awake() {
        _bodyTransform = GetComponentInChildren<BoxCollider2D>().transform;
    }

    private void Start() {
        GameManager.Instance.GetParticipants();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.TryGetComponent(out CarController carController)) {
            
            Vector2 contactToCenterDirection = (_bodyTransform.position - other.transform.position).normalized;

            float dotProduct = Vector2.Dot(transform.up, contactToCenterDirection);

            if (dotProduct < 0) {
                Debug.Log("A participant is cheating");
            }
            else {
                // if player wasn't cheating
                // add a lap to the player
            }
        }
    }
}
