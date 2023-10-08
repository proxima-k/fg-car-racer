using UnityEngine;

public class FinishLine : MonoBehaviour {

    private Transform _bodyTransform;

    private void Awake() {
        _bodyTransform = GetComponentInChildren<BoxCollider2D>().transform;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.transform.root.TryGetComponent(out Participant participant)) {
            
            Vector2 contactToCenterDirection = (_bodyTransform.position - other.transform.position).normalized;

            float dotProduct = Vector2.Dot(transform.up, contactToCenterDirection);

            // Logger.Log("Test");
            
            if (dotProduct < 0) {
                Logger.Log("A participant is cheating");
                participant.ToggleIsCheating();
            }
            else {
                // if player wasn't cheating
                // add a lap to the player
                if (participant.IsCheating) {
                    participant.ToggleIsCheating();
                    return;
                }
                
                participant.AddLapCompleted();
                
                // if laps completed is equal to the required
                // trigger end game
                // GameManager.Instance.TriggerEndGame
                if (participant.LapsCompleted >= MapSettings.Instance.LapsToWin) {
                    GameManager.Instance.TriggerEndGame(participant);
                }
            }
        }
    }
}
