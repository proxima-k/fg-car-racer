using System;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour {
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _quitButton;

    [SerializeField] private GameObject _mapSelectionMenu;
    
    private void Awake() {
        _playButton.onClick.AddListener(ShowMapSelectionMenu);
        _quitButton.onClick.AddListener(Application.Quit);
        
    }

    private void Start() {
        HideMapSelectionMenu();
    }
    
    private void ShowMapSelectionMenu() {
        _mapSelectionMenu.SetActive(true);
    }

    private void HideMapSelectionMenu() {
        _mapSelectionMenu.SetActive(false);
    }
}
