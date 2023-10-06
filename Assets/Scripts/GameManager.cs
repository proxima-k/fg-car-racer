using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager Instance { get; private set; }

    public event EventHandler OnGamePaused;
    public event EventHandler OnGameUnpaused;
    
    private bool isGamePaused;

    public GameMode GameMode => _gameMode;
    [SerializeField] private GameMode _gameMode = GameMode.OnePlayer;
    
    public enum GameState {
        Initialize,
        Countdown,
        Running,
        End
    }
    private GameState _gameState = GameState.Initialize;
    
    // store car controller references
    private List<CarController> _participants = new List<CarController>();
    
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
                ScriptsInitialization();
                
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

    private void ScriptsInitialization() {
        GameInput gameInput = Resources.Load<GameInput>("Prefabs/GameInput");
        Instantiate(gameInput);

        PauseUI pauseUI = Resources.Load<PauseUI>("Prefabs/PauseCanvas");
        Instantiate(pauseUI);
        
        PlayerUI playerUI = Resources.Load<PlayerUI>("Prefabs/PlayerCanvas");
        playerUI = Instantiate(playerUI);
        playerUI.Initialize(_participants);
    }

    private void PlayerInitialization() {
        PlayerInput playerInputPrefab = Resources.Load<PlayerInput>("Prefabs/Car");
        PlayerInput playerInput;
        
        
        Vector3[] startingPositions = MapSettings.Instance.GetStartingPositions();
        Quaternion[] startingRotations = MapSettings.Instance.GetStartingRotations();
        switch (_gameMode) {
            case GameMode.OnePlayer:
                // create single player
                // create a global instance that marks the starting position of the map
                playerInput = Instantiate(playerInputPrefab, startingPositions[0], startingRotations[0]);
                playerInput.SetControlScheme("Player1");

                _participants.Add(playerInput.GetComponent<CarController>());
                break;
                    
            case GameMode.TwoPlayer:
                // Player 1 instantiation
                playerInput = Instantiate(playerInputPrefab, startingPositions[0], startingRotations[0]);
                playerInput.SetControlScheme("Player1");
                _participants.Add(playerInput.GetComponent<CarController>());
                        
                // Player 2 instantiation
                playerInput = Instantiate(playerInputPrefab, startingPositions[1], startingRotations[1]);
                playerInput.SetControlScheme("Player2");
                _participants.Add(playerInput.GetComponent<CarController>());
                break;
                    
            default:
                Logger.LogError("The game mode is not within the defined modes (Player Initialization)");
                break;
        }
    }

    public List<CarController> GetParticipants() {
        return _participants;
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