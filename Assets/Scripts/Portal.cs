using UnityEngine;

public class Portal : MonoBehaviour {

    public Transform Destination;

    GameObject _player;

    void Start() {
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    void Teleport() {
        CharacterController characterController = _player.GetComponent<CharacterController>();
        characterController.enabled = false;
        _player.transform.position = new Vector3(Destination.position.x, Destination.position.y, Destination.position.z);
        characterController.enabled = true;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            Teleport();
        }
    }

}
