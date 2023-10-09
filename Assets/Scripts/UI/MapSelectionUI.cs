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

    [Header("Laps setting")] 
    [SerializeField] private Button _subtractLapButton;
    [SerializeField] private Button _addLapButton;
    [SerializeField] private TextMeshProUGUI _lapsToWinText;
    [SerializeField] private int _maxLapsToWin = 3;
    private int _lapsToWin = 1;
    
    [Header("Map Button Settings")]
    [SerializeField] private GameObject _mapButtonUI;
    [SerializeField] private Button _mapButtonPrefab;

    [Header("")]
    [SerializeField] private Button _backButton;
    
    private GameSettings _gameSettings;
    
    private void Awake() {
        _gameSettings = Resources.Load<GameSettings>("GameSettings");

        _backButton.onClick.AddListener(Hide);

        _gameModeCount = Enum.GetNames(typeof(GameMode)).Length;
        _previousModeButton.onClick.AddListener(PreviousMode);
        _nextModeButton.onClick.AddListener(NextMode);
        
        _addLapButton.onClick.AddListener(AddLap);
        _subtractLapButton.onClick.AddListener(SubtractLap);
    }

    private void Start() {
        // create buttons
        foreach (Object scene in _gameSettings.gameScenes) {
            CreateMapButton(scene.name);
        }
        
        UpdateGameMode();
        UpdateLapCount();
    }

    private void AddLap() {
        if (_lapsToWin < _maxLapsToWin) {
            _lapsToWin++;
        }
        UpdateLapCount();
    }

    private void SubtractLap() {
        if (_lapsToWin > 1) {
            _lapsToWin--;
        }
        UpdateLapCount();
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

    private void UpdateLapCount() {
        _lapsToWinText.text = _lapsToWin.ToString();
    }
    
    private void UpdateGameMode() {
        _gameMode = (GameMode)_currentGameModeIndex;
        _gameModeText.text = Enum.GetName(typeof(GameMode), _currentGameModeIndex);
    }
    
    private void CreateMapButton(string mapName) {
        Button newButton = Instantiate(_mapButtonPrefab, _mapButtonUI.transform, false);
        TextMeshProUGUI buttonText = newButton.GetComponentInChildren<TextMeshProUGUI>();
        buttonText.text = mapName;
        
        newButton.onClick.AddListener(() => SceneHandler.Instance.LoadGameScene(mapName, _gameMode, _lapsToWin));
    }

    private void Hide() {
        gameObject.SetActive(false);
    }
}