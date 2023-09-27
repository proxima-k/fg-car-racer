using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Object = UnityEngine.Object;

public class MapSelectionUI : MonoBehaviour {
    [SerializeField] private Button _backButton;
    
    [Header("Map Button Settings")]
    [SerializeField] private Object[] _scenes;
    [SerializeField] private GameObject _mapButtonUI;
    [SerializeField] private Button _mapButtonPrefab;

    private void Awake() {
        _backButton.onClick.AddListener(Hide);

        foreach (Object scene in _scenes) {
            CreateMapButton(scene.name);
        }
    }

    private void Start() {
        // create buttons
    }

    private void CreateMapButton(string mapName) {
        Button newButton = Instantiate(_mapButtonPrefab, _mapButtonUI.transform, false);
        TextMeshProUGUI buttonText = newButton.GetComponentInChildren<TextMeshProUGUI>();
        buttonText.text = mapName;
        
        newButton.onClick.AddListener(() => SceneManager.LoadScene(mapName));
    }

    private void Hide() {
        gameObject.SetActive(false);
    }
    
    [Serializable]
    private class SceneObject {
        public string Name;
        public Object SceneToLoad;
    }
}