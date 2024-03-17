using System.Collections;
using UnityEngine;

public class RoomDoor : MonoBehaviour, IInteractable {

    public GameObject[] EnemiesInTheRoom;

    public GameObject FloatingTextPrefab;

    public string InteractionPrompt => _isOpen ? "Close Door (E)" : "Open Door (E)";
    public string MissingConditionPrompt = "You must kill all enemies in the room! Before open the door.";

    bool _isOpen = false;
    bool _canOpen = false;
    bool _canInteractable = true;
    GameObject _player;

    void Start() {
        _player = GameObject.FindWithTag("Player");
    }

    public bool Interact(Interactor interactor) {
        if (!_canInteractable) { return false; }
        if (!_canOpen) {
            CheckEnemies();
        }
        if (!_canOpen) {
            _canInteractable = false;
            GameObject floatingTextObject = Instantiate(FloatingTextPrefab, _player.transform.position, Quaternion.identity, _player.transform);
            FloatingText floatingText = floatingTextObject.GetComponent<FloatingText>();
            floatingText.SetText(MissingConditionPrompt);
            floatingText.SetColor(Color.white);
            floatingText._textMeshProUGUI.fontSize -= 5;
            Invoke(nameof(SetCanInteractable), floatingText.DestroyTime);
            return false;
        }

        StartCoroutine(OpenDoor());
        return true;
    }

    void SetCanInteractable() {
        _canInteractable = true;
    }

    IEnumerator OpenDoor() {
        _canInteractable = false;
        _isOpen = !_isOpen;

        Vector3 rotation = Vector3.up * 90.0f;
        if (_isOpen) {
            rotation *= -1;
        }
        Quaternion fromAngle = transform.rotation;
        Quaternion toAngle = Quaternion.Euler(transform.eulerAngles + rotation);
        for (float t = 0f; t < 1; t += Time.deltaTime / 1.0f) {
            transform.rotation = Quaternion.Slerp(fromAngle, toAngle, t);
            yield return null;
        }

        _canInteractable = true;
    }

    bool CheckEnemies() {
        if (EnemiesInTheRoom?.Length <= 0) {
            _canOpen = true;
            return false;
        }

        bool hasEnemy = false;
        foreach (GameObject obj in EnemiesInTheRoom) {
            if (obj != null && obj.GetComponent<Enemy>() != null && obj.activeSelf) {
                hasEnemy = true;
                break;
            }
            if (obj != null && obj.GetComponentsInChildren<Enemy>(false).Length > 0) {
                hasEnemy = true;
                break;
            }
        }
        _canOpen = !hasEnemy;
        return hasEnemy;
    }
}
