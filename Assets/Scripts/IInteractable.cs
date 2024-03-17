using UnityEngine;

public interface IInteractable {
    [HideInInspector]
    string InteractionPrompt { get; }
    bool Interact(Interactor interactor);
}
