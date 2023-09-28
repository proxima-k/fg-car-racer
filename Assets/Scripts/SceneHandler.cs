using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour {
    public static SceneHandler Instance { get; private set; }
    
    private void Awake() {
        if (Instance != null) {
            Destroy(this);
            return;
        }

        Instance = this;
    }

    private void Start() {
        // check if the current scene loaded is within the list
        // if so, create game manager
    }

    // enums
    // main menu
    // game scenes
    // if scene.loaded isn't main menu?
}
