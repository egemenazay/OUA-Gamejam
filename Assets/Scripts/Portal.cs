using UnityEngine;

public class Portal : MonoBehaviour {

    public Transform Destination;

    GameObject _player;
    [SerializeField] private GameObject _npcs1;
    [SerializeField] private GameObject _priest;
    void Start() {
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    void Teleport1() {
        CharacterController characterController = _player.GetComponent<CharacterController>();
        characterController.enabled = false;
        _player.transform.position = new Vector3(Destination.position.x, Destination.position.y, Destination.position.z);
        characterController.enabled = true;
    }
    void Teleport2() {
        CharacterController characterController = _player.GetComponent<CharacterController>();
        characterController.enabled = false;
        _player.transform.position = new Vector3(Destination.position.x, Destination.position.y, Destination.position.z);
        characterController.enabled = true;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            Teleport1();
            _npcs1.SetActive(false);
            _priest.SetActive(true);
        }
    }

}
