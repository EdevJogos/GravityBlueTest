using UnityEngine;

public class Character : MonoBehaviour
{
    public string genericName;
    public string realName;

    protected CharacterMovementHandler _movementHandler;
    protected CharacterInteractionHandler _interactionHandler;

    protected virtual void Awake()
    {
        _movementHandler = GetComponent<CharacterMovementHandler>();
        _interactionHandler = GetComponent<CharacterInteractionHandler>();

        _movementHandler?.Initiate();
        _interactionHandler?.Initiate();
    }

    protected virtual void Start()
    {

    }

    protected virtual void FixedUpdate()
    {
        _movementHandler?.FixedTick();
    }
}
