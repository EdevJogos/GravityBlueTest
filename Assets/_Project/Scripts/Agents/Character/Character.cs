using UnityEngine;

public class Character : MonoBehaviour
{
    public string genericName;
    public string realName;

    protected CharacterMovementHandler _movementHandler;
    protected CharacterInteractionHandler _interactionHandler;
    protected CharacterAnimationHandler _animationHandler;

    protected virtual void Awake()
    {
        _movementHandler = GetComponent<CharacterMovementHandler>();
        _interactionHandler = GetComponent<CharacterInteractionHandler>();
        _animationHandler = GetComponent<CharacterAnimationHandler>();

        _movementHandler?.Initiate();
        _interactionHandler?.Initiate();
        _animationHandler?.Initiate();
    }

    protected virtual void Start()
    {

    }

    protected virtual void Update()
    {
        _animationHandler?.Tick(_movementHandler.input);
    }

    protected virtual void FixedUpdate()
    {
        _movementHandler?.FixedTick();
    }
}
