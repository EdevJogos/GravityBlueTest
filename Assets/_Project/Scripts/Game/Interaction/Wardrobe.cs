using UnityEngine;

public class Wardrobe : MonoBehaviour, IInteract
{
    public event System.Action<Wardrobe> onWardrobeRequested;

    public Interacter interacter;

    private void Awake()
    {
        interacter.ExecuteAction = ExecuteAction;
    }

    public bool CanInteract => true;

    public Interactions GetInteraction()
    {
        return interacter.interaction;
    }

    public Interacter Interact()
    {
        return interacter;
    }

    private void ExecuteAction(Character p_character)
    {
        onWardrobeRequested?.Invoke(this);
    }
}