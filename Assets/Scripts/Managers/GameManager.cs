using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    // Delegates for UI and certain events
    public static GameManager Instance { get; private set; }

    public event EventHandler OnGameStart;
    public event EventHandler OnGameRestart;
    
    public event EventHandler OnGamePaused;
    public event EventHandler OnGameUnpaused;
    private bool isGamePaused;

    public event EventHandler<OnGameEndEventArgs> OnGameEnd;
    public class OnGameEndEventArgs : EventArgs {
        public string winnerName;
        public float timeTaken;
    }
    public event EventHandler<OnCountdownTimerChangedEventArgs> OnCountdownTimerChanged;
    public class OnCountdownTimerChangedEventArgs : EventArgs {
        public float time;
    }
    
    public event EventHandler<OnGameTimerChangedEventArgs> OnGameTimerChanged;
    public class OnGameTimerChangedEventArgs : EventArgs {
        public float time;
    }
    
    // Game settings -----------------------------------
    public GameMode GameMode => _gameMode;
    [SerializeField] private GameMode _gameMode = GameMode.OnePlayer;
    
    public int LapsToWin => _lapsToWin;
    [SerializeField] private int _lapsToWin = 2;
    
    // Car related variables ---------------------------
    private List<Participant> _participants = new List<Participant>();
    private Vector3[] startingPositions;
    private Quaternion[] startingRotations;
    
    // Timer variables ---------------------------------
    [SerializeField] private float _countdownDuration = 3f;
    private float _countdownTimer;
    public float GameTimer => _gameTimer;
    private float _gameTimer = 0f;
    
    // Game state -------------------------------------
    public enum GameState {
        Countdown,
        Running,
        End
    }
    private GameState _gameState = GameState.Countdown;
    
    private void Awake() {
        if (Instance != null) {
            Destroy(this);
            return;
        }
        Instance = this;
    }

    private void Start() {
        // Initialize everything needed for the game
        PlayerInitialization();
        ScriptsInitialization();
        Restart();
    }

    private void Update() {
        switch (_gameState) {
            case GameState.Countdown:
                if (_countdownTimer > 0) {
                    _countdownTimer -= Time.deltaTime;
                    OnCountdownTimerChanged?.Invoke(this, new OnCountdownTimerChangedEventArgs { time = _countdownTimer });
                }
                else {
                    OnGameStart?.Invoke(this, EventArgs.Empty);
                    _gameState = GameState.Running;
                }
                
                break;
            
            // Game running
            case GameState.Running:
                // timer
                _gameTimer += Time.deltaTime;
                OnGameTimerChanged?.Invoke(this, new OnGameTimerChangedEventArgs { time = _gameTimer});
                break;
            
            // When someone wins, display UI for stats
            case GameState.End:
                break;
            
            default:
                Utils.LogWarning("The game state shouldn't reach here!");
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

        GameEndUI gameEndUI = Resources.Load<GameEndUI>("Prefabs/GameEndCanvas");
        Instantiate(gameEndUI);

        CountdownUI countdownUI = Resources.Load<CountdownUI>("Prefabs/CountdownCanvas");
        Instantiate(countdownUI);
    }

    private void PlayerInitialization() {
        PlayerInput playerInputPrefab = Resources.Load<PlayerInput>("Prefabs/Car");
        PlayerInput playerInput;
        Participant participant;
        
        startingPositions = MapSettings.Instance.GetStartingPositions();
        startingRotations = MapSettings.Instance.GetStartingRotations();
        
        switch (_gameMode) {
            case GameMode.OnePlayer:
                // create single player
                // create a global instance that marks the starting position of the map
                playerInput = Instantiate(playerInputPrefab);
                playerInput.SetControlScheme("Player1");

                participant = playerInput.GetComponent<Participant>();
                participant.Name = "Player 1";

                participant.Initialize(startingPositions[0], startingRotations[0]);
                
                _participants.Add(participant);
                break;
                    
            case GameMode.TwoPlayer:
                // Player 1 instantiation
                playerInput = Instantiate(playerInputPrefab);
                playerInput.SetControlScheme("Player1");
                
                participant = playerInput.GetComponent<Participant>();
                participant.Name = "Player 1";
                
                participant.Initialize(startingPositions[0], startingRotations[0]);
                
                _participants.Add(participant);
                
                // Player 2 instantiation
                playerInput = Instantiate(playerInputPrefab);
                playerInput.SetControlScheme("Player2");
                
                participant = playerInput.GetComponent<Participant>();
                participant.Name = "Player 2";
                
                participant.Initialize(startingPositions[1], startingRotations[1]);
                
                _participants.Add(participant);
                break;
                    
            default:
                Utils.LogError("The game mode is not within the defined modes (Player Initialization)");
                break;
        }
    }
    
    public void Restart() {
        _gameState = GameState.Countdown;
        Time.timeScale = 1;
        _countdownTimer = _countdownDuration;
        _gameTimer = 0f;
        
        OnGameRestart?.Invoke(this, EventArgs.Empty);
        OnGameTimerChanged?.Invoke(this, new OnGameTimerChangedEventArgs {time = _gameTimer});
    }
    
    
    public List<Participant> GetParticipants() {
        return _participants;
    }
    
    public void SetLapsToWin(int laps) {
        _lapsToWin = laps;
    }
    
    public void SetGameMode(GameMode gameMode) {
        _gameMode = gameMode;
    }

    public void TriggerEndGame(Participant winner) {
        _gameState = GameState.End;
        
        OnGameEnd?.Invoke(this, new OnGameEndEventArgs {
            winnerName = winner.Name,
            timeTaken = _gameTimer
        });        
    }
    
    public void TogglePauseGame() {
        isGamePaused = !isGamePaused;
        
        Utils.Log("Toggled pause game");
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