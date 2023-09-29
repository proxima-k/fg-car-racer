using UnityEngine;


public static class GameInit {

    [RuntimeInitializeOnLoadMethod]
    public static void Initialize() {
        // Logger.Log("Initializing game");
        GameSettings gameSettings = Resources.Load<GameSettings>("GameSettings");

        SceneHandler sceneHandler = Resources.Load<SceneHandler>("Prefabs/SceneHandler");
        sceneHandler = Object.Instantiate(sceneHandler);
        sceneHandler.Initialize(gameSettings);
    }
}
