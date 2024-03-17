using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interactor : MonoBehaviour {

    public Transform InteractionPoint;
    public float InteractionPointRadius = .5f;
    public LayerMask InteractableMask;

    [DebugState]
    int _numFound = 0;
    readonly Collider[] _colliders = new Collider[3];

    void Update() {
        _numFound = Physics.OverlapSphereNonAlloc(
            InteractionPoint.position,
            InteractionPointRadius,
            _colliders,
            InteractableMask);

        if (_numFound > 0) {
            IInteractable interactable = _colliders[0].GetComponent<IInteractable>();
            if (interactable != null && Keyboard.current.eKey.wasPressedThisFrame) {
                interactable.Interact(this);
            }
        }
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(InteractionPoint.position, InteractionPointRadius);
    }

}
