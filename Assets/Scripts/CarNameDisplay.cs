using System;
using System.Collections;
using TMPro;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.UI;

public class CarNameDisplay : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI _textToDisplay;
    [SerializeField] private RectTransform _background;

    private Transform rootTransform;
    
    private void Start() {
        rootTransform = transform.root;
        _textToDisplay.text = rootTransform.GetComponent<Participant>().Name;

        // needs to wait for a frame before rectTransform is updated
        _textToDisplay.autoSizeTextContainer = true;

        StartCoroutine(ResizeBackground());
    }   
    
    

    private IEnumerator ResizeBackground() {
        yield return null;
        RectTransform textRectTransform = _textToDisplay.rectTransform;
        _background.position = textRectTransform.position;
        _background.sizeDelta = new Vector2(textRectTransform.rect.width + 0.2f, textRectTransform.rect.height + 0.2f);
    }

    private void Update() {
        // rotate in opposite direction of parent
        Quaternion quaternion = Quaternion.Euler(0, 0, -rootTransform.eulerAngles.z);
        transform.localRotation = quaternion;
    }
}
