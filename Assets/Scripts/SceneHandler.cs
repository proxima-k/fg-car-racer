using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour {
    public static SceneHandler Instance { get; private set; }
    
    private GameSettings _gameSettings;
    private GameMode _gameMode;
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

    public void LoadGameScene(GameMode gameMode, string sceneName) {
        _gameMode = gameMode;
        _gameLoadedFromMenu = true;
        SceneManager.LoadScene(sceneName);
    }
    
    private void SceneManager_OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode) {
        Logger.Log(scene.name);
        InitializeScene();
    }
    
    // Initialize scene with necessary scripts and managers
    private void InitializeScene() {
        string currentSceneName = SceneManager.GetActiveScene().name;
        
        foreach (var scene in _gameSettings.gameScenes) {
            // if the scene is a game scene
            if (scene.name == currentSceneName) {
                if (GameManager.Instance == null) {
                    Debug.Log("Game manager instance is null");
                    InstantiateGameManager();
                    return;
                }

                if (_gameLoadedFromMenu) {
                    // set game mode from scene handler
                    GameManager.Instance.SetGameMode(_gameMode);
                    _gameLoadedFromMenu = false;
                }

            }
        }
    }
    
    private void InstantiateGameManager() {
        Logger.Log("Initializing a game manager for the game");
        // set game mode  
        GameManager gameManager = Resources.Load<GameManager>("Prefabs/GameManager");
        gameManager = Instantiate(gameManager);
        gameManager.SetGameMode(_gameMode);
    }
}
