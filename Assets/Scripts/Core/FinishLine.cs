using System;
using UnityEngine;

public class FinishLine : MonoBehaviour {

    private Transform _bodyTransform;
    private bool _hasWinner;
    
    private void Awake() {
        _bodyTransform = GetComponentInChildren<BoxCollider2D>().transform;
    }

    private void Start() {
        GameManager.Instance.OnGameRestart += GameManager_OnGameRestart;
    }

    private void GameManager_OnGameRestart(object sender, EventArgs e) {
        _hasWinner = false;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.transform.root.TryGetComponent(out Participant participant)) {
            
            Vector2 contactToCenterDirection = (_bodyTransform.position - other.transform.position).normalized;
            float dotProduct = Vector2.Dot(transform.up, contactToCenterDirection);
            
            if (dotProduct < 0) {
                // entering from the wrong direction
                Utils.Log("A participant is cheating");
                participant.SetCheating(true);
            }
            else {
                if (participant.IsCheating) {
                    participant.SetCheating(false);
                    return;
                }
                
                // if participant isn't cheating
                participant.AddLapCompleted();
                
                if (!_hasWinner && participant.LapsCompleted >= GameManager.Instance.LapsToWin) {
                    GameManager.Instance.TriggerEndGame(participant);
                    _hasWinner = true;
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.transform.root.TryGetComponent(out Participant participant)) {
            Vector2 centerToContactDirection = (other.transform.position - _bodyTransform.position).normalized;
            float dotProduct = Vector2.Dot(transform.up, centerToContactDirection);

            if (dotProduct >= 0) {
                participant.SetCheating(false);
            }
            else {
                participant.SetCheating(true);
            }
            
        }
    }
}
