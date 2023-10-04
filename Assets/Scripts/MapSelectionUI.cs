using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

public class MapSelectionUI : MonoBehaviour {
    [Header("Mode settings")] 
    [SerializeField] private Button _previousModeButton;
    [SerializeField] private Button _nextModeButton;
    [SerializeField] private TextMeshProUGUI _gameModeText;

    private GameMode _gameMode;
    private int _gameModeCount;
    private int _currentGameModeIndex = 0;
    
    [SerializeField] private Button _backButton;
    
    [Header("Map Button Settings")]
    [SerializeField] private GameObject _mapButtonUI;
    [SerializeField] private Button _mapButtonPrefab;

    private GameSettings _gameSettings;
    
    private void Awake() {
        _gameSettings = Resources.Load<GameSettings>("GameSettings");

        _backButton.onClick.AddListener(Hide);

        _gameModeCount = Enum.GetNames(typeof(GameMode)).Length;
        _previousModeButton.onClick.AddListener(PreviousMode);
        _nextModeButton.onClick.AddListener(NextMode);
    }

    private void Start() {
        // create buttons
        foreach (Object scene in _gameSettings.gameScenes) {
            CreateMapButton(scene.name);
        }
        
        UpdateGameMode();
    }

    private void PreviousMode() {
        if (_currentGameModeIndex > 0) {
            _currentGameModeIndex--;
            UpdateGameMode();
        }
    }

    private void NextMode() {
        if (_currentGameModeIndex < _gameModeCount-1) {
            _currentGameModeIndex++;
            UpdateGameMode();
        }
    }
    
    private void UpdateGameMode() {
        _gameMode = (GameMode)_currentGameModeIndex;
        _gameModeText.text = Enum.GetName(typeof(GameMode), _currentGameModeIndex);
    }
    
    private void CreateMapButton(string mapName) {
        Button newButton = Instantiate(_mapButtonPrefab, _mapButtonUI.transform, false);
        TextMeshProUGUI buttonText = newButton.GetComponentInChildren<TextMeshProUGUI>();
        buttonText.text = mapName;
        
        newButton.onClick.AddListener(() => SceneHandler.Instance.LoadGameScene(_gameMode, mapName));
    }

    private void Hide() {
        gameObject.SetActive(false);
    }
}