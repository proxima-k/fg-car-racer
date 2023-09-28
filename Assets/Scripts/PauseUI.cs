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
        GameManager.Instance.OnGamePaused += GameManager_OnGamePaused;
        GameManager.Instance.OnGameUnpaused += GameManager_OnGameUnpaused;
        _resumeButton.onClick.AddListener(GameManager.Instance.TogglePauseGame);
        
        Hide();
    }

    private void GameManager_OnGamePaused(object sender, EventArgs e) {
        Show();
    }
    
    private void GameManager_OnGameUnpaused(object sender, EventArgs e) {
        Hide();
    }

    private void Show() {
        gameObject.SetActive(true);
    }

    private void Hide() {
        gameObject.SetActive(false);
    }
}
