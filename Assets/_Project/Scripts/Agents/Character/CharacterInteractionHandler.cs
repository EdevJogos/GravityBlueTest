using UnityEngine;

public class CharacterInteractionHandler : MonoBehaviour, IInteract
{
    public float interactionRange;
    public Interacter interacter;

    public virtual bool CanInteract => false;

    public virtual void Initiate() { }

    public virtual void RequestInteraction() { }

    public Interactions GetInteraction()
    {
        return interacter.interaction;
    }

    public Interacter Interact()
    {
        return interacter;
    }
}