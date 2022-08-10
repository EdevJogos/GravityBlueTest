using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputsHandler : MonoBehaviour, IInputHandler
{
    public event System.Action<Vector2> onMovementPerformed;
    public event System.Action onInteractRequested;

    private PlayerControls _inputActions;

    public void Initiate(PlayerControls p_inputActions)
    {
        _inputActions = p_inputActions;

        _inputActions.Player.Movement.performed += Movement_performed;
        _inputActions.Player.Movement.canceled += Movement_canceled;
        _inputActions.Player.Movement.Enable();

        _inputActions.Player.Interact.performed += Interact_performed;
        _inputActions.Player.Interact.Enable();
    }

    public void Enable(bool p_enable)
    {
        if (p_enable) _inputActions.Player.Enable(); else _inputActions.Player.Disable();
    }

    private void Movement_performed(InputAction.CallbackContext p_context)
    {
        onMovementPerformed?.Invoke(p_context.ReadValue<Vector2>());
    }

    private void Movement_canceled(InputAction.CallbackContext p_context)
    {
        onMovementPerformed?.Invoke(Vector2.zero);
    }

    private void Interact_performed(InputAction.CallbackContext p_context)
    {
        onInteractRequested?.Invoke();
    }
}
