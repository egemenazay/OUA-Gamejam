using UnityEngine;

public class Teleporter : MonoBehaviour, IInteractable {
    public Transform Destination;
    GameObject _player;

    [SerializeField] string _interactionPrompt;
    public string InteractionPrompt => _interactionPrompt;

    void Start() {
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    public bool Interact(Interactor interactor) {
        Teleport();
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
