using UnityEngine;

public class Player : Character
{
    public PlayerInputsHandler playerInputHandler;

    protected override void Awake()
    {
        base.Awake();

        playerInputHandler.onMovementPerformed += PlayerInputHandler_onMovementPerformed;
        playerInputHandler.onInteractRequested += _playerInputHandler_onInteractRequested;

        playerInputHandler.Initiate();
    }

    

    protected override void Start()
    {
        base.Start();
    }

    #region INPUT_HANDLER

    private void PlayerInputHandler_onMovementPerformed(Vector2 p_input)
    {
        _movementHandler.SetInput(p_input);
    }

    private void _playerInputHandler_onInteractRequested()
    {
        _interactionHandler.RequestInteraction();
    }

    #endregion
}
