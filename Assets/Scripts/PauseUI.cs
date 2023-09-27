using System;
using UnityEngine;
using UnityEngine.UI;



public class PauseUI : MonoBehaviour {

    [SerializeField] private Button _resumeButton;
    [SerializeField] private Button _mainMenuButton;

    private void Awake() {
        // _resumeButton.onClick.AddListener();
            // game manager toggle pause
        // _mainMenuButton.onClick.AddListener();
    }

    private void Start() {
        Hide();
    }

    private void Show() {
        gameObject.SetActive(true);
    }

    private void Hide() {
        gameObject.SetActive(false);
    }
}
