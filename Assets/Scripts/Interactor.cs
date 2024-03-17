using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.InputSystem;


public class Interactor : MonoBehaviour {

    public Transform InteractionPoint;
    public float InteractionPointRadius = .5f;
    public LayerMask InteractableMask;
    public InteractionPromptUI InteractionPromptUI;

    //[DebugState]
    int _numFound = 0;
    readonly Collider[] _colliders = new Collider[3];
    IInteractable _interactable;

    void Update() {
        _numFound = Physics.OverlapSphereNonAlloc(
            InteractionPoint.position,
            InteractionPointRadius,
            _colliders,
            InteractableMask);

        if (_numFound > 0) {
            _interactable = _colliders[0].GetComponent<IInteractable>();
            if (_interactable != null) {
                if (!InteractionPromptUI.IsDisplayed) {
                    InteractionPromptUI.SetUp(_interactable.InteractionPrompt);
                }

                if (Keyboard.current.eKey.wasPressedThisFrame) {
                    _interactable.Interact(this);
                }
            }
        }
        else if (InteractionPromptUI.IsDisplayed) {
            InteractionPromptUI.Close();
        }
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(InteractionPoint.position, InteractionPointRadius);
    }

}
