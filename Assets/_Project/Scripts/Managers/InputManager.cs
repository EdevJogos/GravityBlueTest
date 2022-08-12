using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public event System.Action onPauseRequested;

    public static Vector2 MouseWorld { get { return CameraManager.MainCamera.ScreenToWorldPoint(Input.mousePosition); } }

    public DialogInputsHandler dialogInputs;
    public ShopInputHandler shopInputs;
    public PlayerInputsHandler playerInputs;

    private IInputHandler _curInputHandler;
    private PlayerControls _inputActions;

    public void Initiate()
    {
        _inputActions = new PlayerControls();

        shopInputs.Initiate(_inputActions);
        dialogInputs.Initiate(_inputActions);
        playerInputs.Initiate(_inputActions);
    }

    public void Initialize()
    {
        _curInputHandler = playerInputs;
    }

    /// <summary>
    /// Switch active inputs callbacks.
    /// </summary>
    public void SwitchInput(Inputs p_input)
    {
        _curInputHandler.Enable(false);

        switch (p_input)
        {
            case Inputs.PLAYER:
                playerInputs.Enable(true);
                _curInputHandler = playerInputs;
                break;
            case Inputs.DIALOG:
                dialogInputs.Enable(true);
                _curInputHandler = dialogInputs;
                break;
            case Inputs.SHOP:
                shopInputs.Enable(true);
                _curInputHandler = shopInputs;
                break;
        }
    }
}