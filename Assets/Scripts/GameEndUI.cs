using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameEndUI : MonoBehaviour {
    [SerializeField] private Button _againButton;
    [SerializeField] private Button _mainMenuButton;
    [SerializeField] private GameObject _gameEndUI;
    [SerializeField] private TextMeshProUGUI _winnerText;
    [SerializeField] private TextMeshProUGUI _timerText;

    private void Awake() {
        Hide();
    }

    private void Start() {
        GameManager.Instance.OnGameEnd += GameManager_OnGameEnd;
        _mainMenuButton.onClick.AddListener(SceneHandler.Instance.LoadMainMenuScene);
    }

    private void GameManager_OnGameEnd(object sender, GameManager.OnGameEndEventArgs e) {
        Show();
        _winnerText.text = $"Winner: {e.winnerName}!";
    }
    
    private void Show() {
        _gameEndUI.SetActive(true);
    }

    private void Hide() {
        _gameEndUI.SetActive(false);
    }
}
