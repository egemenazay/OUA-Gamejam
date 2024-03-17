using UnityEngine;

public class Teleporter : MonoBehaviour, IInteractable {
    public Transform Destination;
    GameObject _player;

    [SerializeField] string _interactionPrompt;
    public string InteractionPrompt => _interactionPrompt;

    public bool FromDungeon;
    public GameObject _npcs1;
    public GameObject _priest;

    void Start() {
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    public bool Interact(Interactor interactor) {
        if (FromDungeon) {
            // TODO: check all tasks finished
            if (false) {
                // show floating text
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

}
