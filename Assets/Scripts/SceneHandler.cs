using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour {
    public static SceneHandler Instance { get; private set; }

    private GameSettings _gameSettings;
    
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
        
        VerifyCurrentScene();
    }

    public void LoadMainMenuScene() {
        Logger.Log((_gameSettings!=null).ToString());
        SceneManager.LoadScene(_gameSettings.mainMenuScene.name);
    }
    
    private void SceneManager_OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode) {
        Logger.Log(scene.name);
        VerifyCurrentScene();
    }
    
    private void VerifyCurrentScene() {
        string currentSceneName = SceneManager.GetActiveScene().name;
        
        foreach (var scene in _gameSettings.gameScenes) {
            if (scene.name == currentSceneName) {
                InstantiateGameManager();
                return;
            }
        }
    }

    private void InstantiateGameManager() {
        // Logger.Log("Initializing a game manager for the game");
    }
}
