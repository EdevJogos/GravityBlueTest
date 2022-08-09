using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputsHandler : MonoBehaviour, IInputHandler
{
    public event System.Action<Vector2> onMovementPerformed;
    public event System.Action onInteractRequested;

    public PlayerControls inputActions;

    public void Initiate()
    {
        inputActions = new PlayerControls();

        inputActions.Player.Movement.performed += Movement_performed;
        inputActions.Player.Movement.canceled += Movement_canceled;
        inputActions.Player.Movement.Enable();

        inputActions.Player.Interact.performed += Interact_performed;
        inputActions.Player.Interact.Enable();
    }

    public void Enable(bool p_enable)
    {
        if (p_enable) inputActions.Player.Enable(); else inputActions.Player.Disable();
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
