using UnityEngine;
using UnityEngine.InputSystem;

public class ShopInputHandler : MonoBehaviour, IInputHandler
{
    public event System.Action<Vector2> onSelectOptionRequested;
    public event System.Action onConfirmRequested;

    private PlayerControls _inputActions;

    public void Initiate(PlayerControls p_inputActions)
    {
        _inputActions = p_inputActions;

        _inputActions.Shop.Confirm.performed += Confirm_performed;
        _inputActions.Shop.Confirm.Enable();

        _inputActions.Shop.Select.performed += Select_performed;
        _inputActions.Shop.Select.Enable();

        Enable(false);
    }

    public void Enable(bool p_enable)
    {
        if (p_enable) _inputActions.Shop.Enable(); else _inputActions.Shop.Disable();
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