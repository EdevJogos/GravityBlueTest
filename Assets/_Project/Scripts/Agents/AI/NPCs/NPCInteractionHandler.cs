using System.Collections.Generic;
using UnityEngine;

public class NPCInteractionHandler : CharacterInteractionHandler
{
    public event System.Action onShopRequested;
    public event System.Action<Character, Dialog> onDialogRequested;
    public event System.Action<Dialog> onTalkRequested;
    public event System.Action onExitDialogRequested;

    [NonReorderable] public Dialog[] greetings;
    [NonReorderable] public Dialog[] talkDialogs;

    [System.NonSerialized] public bool know = false;
    [System.NonSerialized] public string knownName = "";
    private int _talkIndex = 0;
    private List<int> _choosen = new List<int>();

    public override void Initiate()
    {
        base.Initiate();

        interacter.ExecuteAction = Greet;
    }

    private void Greet(Character p_other)
    {
        Debug.Log("NPC Greet");
        AudioManager.PlaySFX(SFXOccurrence.NPC_HELLO);
        onDialogRequested?.Invoke(p_other, greetings[know ? 1 : 0]);
    }

    private void ExecuteAction(DialogResult p_result)
    {
        switch (p_result.action)
        {
            case DialogActions.TALK:
                onTalkRequested?.Invoke(talkDialogs[_talkIndex]);
                know = true;
                break;
            case DialogActions.OPEN_SHOP:
                Debug.Log("Open Shop");
                onShopRequested?.Invoke();
                break;
            case DialogActions.CONTINUE_TALK:
                ContinueTalk();
                break;
            case DialogActions.SET_KNOWN_NAME:
                knownName = talkDialogs[_talkIndex].choices[p_result.data].choiceText;
                ContinueTalk();
                break;
            case DialogActions.EXIT:
                _talkIndex = 0;
                onExitDialogRequested?.Invoke();
                break;
        }
    }

    public void OnDialogResultRecieved(DialogResult p_result)
    {
        Debug.Log("OnDialogResultRecieved" + p_result.action);

        ExecuteAction(p_result);
    }

    private void ContinueTalk()
    {
        _talkIndex++;
        onTalkRequested?.Invoke(talkDialogs[_talkIndex]);
    }
}