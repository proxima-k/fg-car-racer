using System;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager Instance { get; private set; }

    public event EventHandler OnGamePaused;
    public event EventHandler OnGameUnpaused;

    private bool isGamePaused;
    
    [SerializeField] private GameMode _gameMode = GameMode.OnePlayer;
    
    public enum GameState {
        Initialize,
        Countdown,
        Running,
        End
    }
    private GameState _gameState = GameState.Initialize;
    
    private void Awake() {
        if (Instance != null) {
            Destroy(this);
            return;
        }
        Instance = this;
        // DontDestroyOnLoad(this);
    }

    private void Update() {
        switch (_gameState) {
            // Initialize everything needed for the game
            case GameState.Initialize:
                PlayerInitialization();
                _gameState = GameState.Countdown;
                break;
            
            // Countdown till the game starts
            case GameState.Countdown:
                // do countdown
                break;
            
            // Game running
            case GameState.Running:
                // timer
                break;
            
            // When someone wins, display UI for stats
            case GameState.End:
                break;
            
            default:
                Logger.LogWarning("The game state shouldn't reach here!");
                break;
        }
    }

    private void PlayerInitialization() {
        PlayerInput playerInputPrefab = Resources.Load<PlayerInput>("Prefabs/Car");
        PlayerInput playerInput;

        Vector3[] startingPositions = MapSettings.Instance.GetStartingPositions();
        switch (_gameMode) {
            case GameMode.OnePlayer:
                // create single player
                // create a global instance that marks the starting position of the map
                playerInput = Instantiate(playerInputPrefab, startingPositions[0], Quaternion.identity);
                playerInput.SetControlScheme("Player1");
                break;
                    
            case GameMode.TwoPlayer:
                // Player 1 instantiation
                playerInput = Instantiate(playerInputPrefab, startingPositions[0], Quaternion.identity);
                playerInput.SetControlScheme("Player1");
                        
                // Player 2 instantiation
                playerInput = Instantiate(playerInputPrefab, startingPositions[1], Quaternion.identity);
                playerInput.SetControlScheme("Player2");
                break;
                    
            default:
                Logger.LogWarning("The game mode is not within the defined (Player Initialization)");
                break;
        }
    }
    
    public void SetGameMode(GameMode gameMode) {
        _gameMode = gameMode;
    }
    
    public void TogglePauseGame() {
        isGamePaused = !isGamePaused;
        
        Logger.Log("Toggled pause game");
        if (isGamePaused) {
            Time.timeScale = 0f;
            OnGamePaused?.Invoke(this, EventArgs.Empty);
        }
        else {
            Time.timeScale = 1f;
            OnGameUnpaused?.Invoke(this, EventArgs.Empty);
        }
    }
}


public enum GameMode {
    OnePlayer,
    TwoPlayer
}