using System;
using UnityEngine;
using UnityEngine.UI;

public class PauseUI : MonoBehaviour {

    [SerializeField] private Button _resumeButton;
    [SerializeField] private Button _mainMenuButton;
    [SerializeField] private GameObject _pauseUI;
    
    private void Awake() {
    }

    private void Start() {
        _resumeButton.onClick.AddListener(GameManager.Instance.TogglePauseGame);
        _mainMenuButton.onClick.AddListener(SceneHandler.Instance.LoadMainMenuScene);
        _mainMenuButton.onClick.AddListener(GameManager.Instance.TogglePauseGame);
        GameManager.Instance.OnGamePaused += GameManager_OnGamePaused;
        GameManager.Instance.OnGameUnpaused += GameManager_OnGameUnpaused;
        
        Hide();
    }

    private void GameManager_OnGamePaused(object sender, EventArgs e) {
        Show();
    }
    
    private void GameManager_OnGameUnpaused(object sender, EventArgs e) {
        Hide();
    }

    private void Show() {
        _pauseUI.SetActive(true);
    }

    private void Hide() {
        _pauseUI.SetActive(false);
    }

    private void OnDestroy() {
        _resumeButton.onClick.RemoveListener(GameManager.Instance.TogglePauseGame);
        _mainMenuButton.onClick.RemoveListener(SceneHandler.Instance.LoadMainMenuScene);
        _mainMenuButton.onClick.RemoveListener(GameManager.Instance.TogglePauseGame);
        GameManager.Instance.OnGamePaused -= GameManager_OnGamePaused;
        GameManager.Instance.OnGameUnpaused -= GameManager_OnGameUnpaused;
    }
}
