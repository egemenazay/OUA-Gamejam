using UnityEngine;

public class Teleporter : MonoBehaviour, IInteractable {
    public Transform Destination;
    GameObject _player;

    [SerializeField] string _interactionPrompt;
    public string InteractionPrompt => _interactionPrompt;

    public bool FromDungeon;
    public GameObject _npcs1;
    public GameObject _priest;
    public GameObject FloatingTextPrefab;
    public string MissingConditionPrompt = "You must collect all powerups before turn back to the village!";
    bool _canInteractable = true;

    void Start() {
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    public bool Interact(Interactor interactor) {
        if (!_canInteractable) { return false; }

        if (FromDungeon) {
            int powerUpsCollected = _player.GetComponent<Player>().powerUpsCollected;
            // TODO: check all tasks finished
            if (powerUpsCollected < 3) {
                _canInteractable = false;
                // show floating text
                GameObject floatingTextObject = Instantiate(FloatingTextPrefab, _player.transform.position, Quaternion.identity, _player.transform);
                FloatingText floatingText = floatingTextObject.GetComponent<FloatingText>();
                floatingText.SetText(MissingConditionPrompt);
                floatingText.SetColor(Color.white);
                floatingText._textMeshProUGUI.fontSize -= 5;
                Invoke(nameof(SetCanInteractable), floatingText.DestroyTime);
                return false;
            }
            Teleport();
            _npcs1.SetActive(false);
            _priest.SetActive(true);
        } else {
            Teleport();
        }
        return true;
    }

    void Teleport() {
        CharacterController characterController = _player.GetComponent<CharacterController>();
        characterController.enabled = false;
        _player.transform.position = new Vector3(
            Destination.position.x,
            Destination.position.y,
            Destination.position.z);
        characterController.enabled = true;
    }

    void SetCanInteractable() {
        _canInteractable = true;
    }

}
