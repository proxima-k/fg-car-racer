using UnityEngine;

[CreateAssetMenu(menuName = "GameSettings", fileName = "GameSettings")]
public class GameSettings : ScriptableObject {
    
    // for easier scene management to initialize required services like game manager, etc.
    [Header("Scene settings")]
    public Object mainMenuScene;
    public Object[] gameScenes;
}
