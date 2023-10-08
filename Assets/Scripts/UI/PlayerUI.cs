using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour {
    [SerializeField] private GameObject _player1UI;
    [SerializeField] private GameObject _player2UI;
    
    [SerializeField] private Image _player1FuelBar;
    [SerializeField] private Image _player2FuelBar;

    [SerializeField] private TextMeshProUGUI _timerText;
    
    
    public void Initialize(List<CarController> carControllers) {

        GameManager.Instance.OnGameTimerChanged += GameManager_OnGameTimerChanged;
        switch (carControllers.Count) {
            case 0:
                Logger.LogError("There are no participants.");
                return;
            case 1:
                carControllers[0].OnFuelChanged += Player1_OnFuelChanged;
                _player1UI.SetActive(true);
                _player2UI.SetActive(false);
                
                break;
            case 2:
                carControllers[0].OnFuelChanged += Player1_OnFuelChanged;
                carControllers[1].OnFuelChanged += Player2_OnFuelChanged;
                
                _player1UI.SetActive(true);
                _player2UI.SetActive(true);
                
                break;
            default:
                Logger.LogWarning("There are extra participants in the game.");
                break;
        }
    }

    private void GameManager_OnGameTimerChanged(object sender, GameManager.OnGameTimerChangedEventArgs e) {

        // float time = e.time * 100f;
        int minute = (int)(e.time / 60);
        int seconds = (int)(e.time % 60);
        int milliseconds = (int)(e.time * 100 % 100);
        _timerText.text = $"{minute:00}:{seconds:00}:{milliseconds:00}";
    }

    
    private void Player1_OnFuelChanged(object sender, CarController.OnFuelChangedEventArgs e) {
        _player1FuelBar.fillAmount = e.fuelNormalized;
    }
    private void Player2_OnFuelChanged(object sender, CarController.OnFuelChangedEventArgs e) {
        _player2FuelBar.fillAmount = e.fuelNormalized;
    }
}
