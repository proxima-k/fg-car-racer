using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CountdownUI : MonoBehaviour {
    [SerializeField] private GameObject _countdownUI;
    [SerializeField] private TextMeshProUGUI _countdownText;
    [SerializeField] private Image _backgroundImage;

    [SerializeField] private float _fadeDuration = 1f;
    private float _goTextDuration = 1f;
    private float _goTimer;
    private float _fadeTimer;
    private bool _gameStarted;

    private Color _countdownTextColor;
    private Color _backgroundImageColor;
    
    private void Awake() {
        GameManager.Instance.OnCountdownTimerChanged += GameManager_OnCountdownTimerChanged;
        GameManager.Instance.OnGameStart += GameManager_OnGameStart;
        GameManager.Instance.OnGameRestart += GameManager_OnGameRestart;
        
        _countdownTextColor = _countdownText.color;
    }

    private void Update() {
        if (!_gameStarted)
            return;

        // handles the GO! display
        if (_goTimer <= 0) {
            if (_fadeTimer > 0) {
                _fadeTimer -= Time.deltaTime;

                float updateAmount = Time.deltaTime / _fadeDuration;
                
                _countdownTextColor.a -= updateAmount;
                _countdownText.color = _countdownTextColor;

                _backgroundImageColor.a -= updateAmount;
                _backgroundImage.color = _backgroundImageColor;
            }
            else {
                _gameStarted = false;
                Hide();
            }
        }
        else {
            _goTimer -= Time.deltaTime;
        }
    }

    private void GameManager_OnGameRestart(object sender, EventArgs e) {
        Show();
    }

    private void GameManager_OnGameStart(object sender, EventArgs e) {
        _countdownText.text = "GO!";
        _fadeTimer = _fadeDuration;
        _goTimer = _goTextDuration;
        _gameStarted = true;
    }

    private void GameManager_OnCountdownTimerChanged(object sender, GameManager.OnCountdownTimerChangedEventArgs e) {
        int seconds = Mathf.CeilToInt(e.time);
        _countdownText.text = seconds.ToString();
    }

    
    private void Show() {
        _countdownTextColor.a = 1;
        _countdownText.color = _countdownTextColor;

        _backgroundImageColor.a = 1;
        _backgroundImage.color = _backgroundImageColor;
        
        _countdownUI.SetActive(true);
    }

    private void Hide() {
        _countdownUI.SetActive(false);
    }
}
