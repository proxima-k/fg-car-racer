using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager Instance { get; private set; }

    public event EventHandler OnGamePaused;
    public event EventHandler OnGameUnpaused;
    private bool isGamePaused;

    public event EventHandler OnGameStart;
    

    public GameMode GameMode => _gameMode;
    [SerializeField] private GameMode _gameMode = GameMode.OnePlayer;
    
    public enum GameState {
        Countdown,
        Running,
        End
    }
    private GameState _gameState = GameState.Countdown;
    
    private List<CarController> _participants = new List<CarController>();
    
    private float _countdownTimer;
    public float GameTimer => _gameTimer;
    private float _gameTimer = 0f;
    
    private void Awake() {
        if (Instance != null) {
            Destroy(this);
            return;
        }
        Instance = this;
        // DontDestroyOnLoad(this);
    }

    private void Start() {
        // Initialize everything needed for the game
        PlayerInitialization();
        ScriptsInitialization();
    }

    private void Update() {
        switch (_gameState) {
            case GameState.Countdown:
                // do countdown
                break;
            
            // Game running
            case GameState.Running:
                // timer
                _gameTimer += Time.deltaTime;
                
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