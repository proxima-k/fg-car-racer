using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour {
    public static SceneHandler Instance { get; private set; }
    
    private GameSettings _gameSettings;
    private GameMode _gameMode;
    private int _lapsToWin = 1;
    private bool _gameLoadedFromMenu;
    
    private void Awake() {
        if (Instance != null) {
            Destroy(this);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(this);
    }

    
    public void Initialize(GameSettings gameSettings) {
        _gameSettings = gameSettings;
        SceneManager.sceneLoaded += SceneManager_OnSceneLoaded;
        
        InitializeScene();
    }

    public void LoadMainMenuScene() {
        SceneManager.LoadScene(_gameSettings.mainMenuScene.name);
    }

    public void LoadGameScene(string sceneName, GameMode gameMode, int lapsToWin) {
        _gameMode = gameMode;
        _lapsToWin = lapsToWin;
        _gameLoadedFromMenu = true;
        SceneManager.LoadScene(sceneName);
    }
    
    private void SceneManager_OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode) {
        Utils.Log($"Loaded scene: {scene.name}");
        InitializeScene();
    }
    
    // Initialize scene with necessary scripts and managers
    private void InitializeScene() {
        string currentSceneName = SceneManager.GetActiveScene().name;
        
        foreach (var scene in _gameSettings.gameScenes) {
            // if the scene is a game scene
            if (scene.name == currentSceneName) {
                if (GameManager.Instance == null) {
                    Utils.Log($"No existing game manager, creating a new one with default mode: <{_gameMode}> and default number of laps to win: <{_lapsToWin}>");
                    InstantiateGameManager();
                }

                if (_gameLoadedFromMenu) {
                    GameManager.Instance.SetGameMode(_gameMode);
                    GameManager.Instance.SetLapsToWin(_lapsToWin);
                    
                    _gameLoadedFromMenu = false;
                }

            }
        }
    }
    
    private void InstantiateGameManager() {
        Utils.Log("Initializing a game manager for the game");

        GameManager gameManager = Resources.Load<GameManager>("Prefabs/GameManager");
        gameManager = Instantiate(gameManager);
        gameManager.SetGameMode(_gameMode);
        gameManager.SetLapsToWin(_lapsToWin);
    }
}
