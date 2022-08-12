using UnityEngine;
using UnityEngine.InputSystem;

public class DialogInputsHandler : MonoBehaviour, IInputHandler
{
    public event System.Action<Vector2> onSelectOptionRequested;
    public event System.Action onConfirmRequested;

    private PlayerControls _inputActions;

    public void Initiate(PlayerControls p_inputActions)
    {
        _inputActions = p_inputActions;

        _inputActions.Dialog.Confirm.performed += Confirm_performed;
        _inputActions.Dialog.Confirm.Enable();

        _inputActions.Dialog.Select.performed += Select_performed;
        _inputActions.Dialog.Select.Enable();

        Enable(false);
    }

    public void Enable(bool p_enable)
    {
        if (p_enable) _inputActions.Dialog.Enable(); else _inputActions.Dialog.Disable();
    }

    private void Select_performed(InputAction.CallbackContext p_context)
    {
        onSelectOptionRequested?.Invoke(p_context.ReadValue<Vector2>());
    }

    private void Confirm_performed(InputAction.CallbackContext p_context)
    {
        onConfirmRequested?.Invoke();
    }
}
