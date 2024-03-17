using TMPro;
using UnityEngine;

public class FloatingText : MonoBehaviour {
    public float DestroyTime = 3.0f;
    public Vector3 Offset = new Vector3(0, 2, 0);
    public Vector3 RandomizeIntensity = new Vector3(0.5f, 0, 0);

    public TextMeshProUGUI _textMeshProUGUI;

    Camera _camera;

    private void Start() {
        _camera = Camera.main;
        _textMeshProUGUI = GetComponentInChildren<TextMeshProUGUI>();
        Destroy(gameObject, DestroyTime);
        transform.localPosition += Offset;
        transform.localPosition += new Vector3(
            Random.Range(-RandomizeIntensity.x, RandomizeIntensity.x),
            Random.Range(-RandomizeIntensity.y, RandomizeIntensity.y),
            Random.Range(-RandomizeIntensity.z, RandomizeIntensity.z)
        );
    }

    private void LateUpdate() {
        Quaternion rotation = _camera.transform.rotation;
        transform.LookAt(transform.position + rotation * Vector3.forward, rotation * Vector3.up);
    }

    public void SetText(string text) {
        _textMeshProUGUI.text = text;
    }

}
