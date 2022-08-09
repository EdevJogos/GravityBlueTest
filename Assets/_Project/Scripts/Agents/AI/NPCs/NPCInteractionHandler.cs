using System.Collections.Generic;
using UnityEngine;

public class NPCInteractionHandler : CharacterInteractionHandler
{
    public event System.Action<Character, Dialog, System.Action<DialogResult>> onDialogRequested;

    [NonReorderable] public Dialog[] greetings;
    [NonReorderable] public Dialog[] talkDialogs;

    [System.NonSerialized ] public bool know = false;
    private int _interaction = 0;
    private List<int> _choosen = new List<int>();

    public override void Initiate()
    {
        base.Initiate();

        interacter.ExecuteAction = Greet;
    }

    private void Greet(Character p_other)
    {
        Debug.Log("NPC Greet");
        onDialogRequested?.Invoke(p_other, greetings[know ? 1 : 0], OnDialogResultRecieved);
    }

    private void ExecuteAction(DialogResult p_result)
    {
        switch (p_result.action)
        {
            case DialogActions.TALK:
                break;
            case DialogActions.OPEN_SHOP:
                Debug.Log("Open Shop");
                break;
            case DialogActions.CONTINUE_DIALOG:
                break;
            case DialogActions.EXIT:
                break;
        }
    }

    private void OnDialogResultRecieved(DialogResult p_result)
    {
        Debug.Log("OnDialogResultRecieved" + p_result.action);

        _interaction++;

        ExecuteAction(p_result);
    }
}