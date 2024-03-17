using TMPro;
using UnityEngine;

public class InteractionPromptUI : MonoBehaviour {
    Camera _camera;
    public GameObject UIPanel;
    public TextMeshProUGUI PromptText;
    public bool IsDisplayed;

    private void Start() {
        _camera = Camera.main;
        UIPanel.SetActive(false);
    }

    private void LateUpdate() {
        Quaternion rotation = _camera.transform.rotation;
        transform.LookAt(transform.position + rotation * Vector3.forward, rotation * Vector3.up);
    }

    public void SetUp(string promptText) {
        PromptText.text = promptText;
        UIPanel.SetActive(true);
        IsDisplayed = true;
    }

    public void Close() {
        IsDisplayed = false;
        UIPanel.SetActive(false);
    }
}