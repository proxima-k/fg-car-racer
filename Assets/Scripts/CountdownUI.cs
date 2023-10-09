using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CountdownUI : MonoBehaviour {
    [SerializeField] private GameObject _countdownUI;
    [SerializeField] private TextMeshProUGUI _countdownText;

    private void Start() {
        GameManager.Instance.OnCountdownTimerChanged += GameManager_OnCountdownTimerChanged;
        GameManager.Instance.OnGameStart += GameManager_OnGameStart;
    }

    private void GameManager_OnGameStart(object sender, EventArgs e) {
        // add go coroutine?
        Hide();
    }

    private void GameManager_OnCountdownTimerChanged(object sender, GameManager.OnCountdownTimerChangedEventArgs e) {
        int seconds = Mathf.CeilToInt(e.time);
        _countdownText.text = seconds.ToString();
    }

    private void Show() {
        _countdownUI.SetActive(true);
    }

    private void Hide() {
        _countdownUI.SetActive(false);
    }
}
