using System;
using TMPro;
using UnityEngine;

public class CarNameDisplay : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI _textToDisplay;

    private Transform rootTransform;

    private void Start() {
        rootTransform = transform.root;
        _textToDisplay.text = rootTransform.GetComponent<Participant>().Name;
    }

    private void Update() {
        // rotate in opposite direction of parent
        Quaternion quaternion = Quaternion.Euler(0, 0, -rootTransform.eulerAngles.z);
        transform.localRotation = quaternion;
    }
}
