using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public event System.Action onPauseRequested;

    public static Vector2 MouseWorld { get { return CameraManager.MainCamera.ScreenToWorldPoint(Input.mousePosition); } }

    public DialogInputsHandler dialogInputs;
    public PlayerInputsHandler playerInputs;

    private IInputHandler _curInputHandler;

    public void Initialize()
    {
        _curInputHandler = playerInputs;
    }

    public void SwitchInput(Inputs p_input)
    {
        _curInputHandler.Enable(false);

        switch (p_input)
        {
            case Inputs.PLAYER:
                playerInputs.Enable(true);
                GetComponent<PlayerInput>().SwitchCurrentActionMap("Player");
                break;
            case Inputs.DIALOG:
                dialogInputs.Enable(true);
                GetComponent<PlayerInput>().SwitchCurrentActionMap("Dialog");
                break;
        }
    }
}