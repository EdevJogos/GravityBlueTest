using UnityEngine;

public class NPC : Character
{
    public static event System.Action<string, string, Dialog, System.Action<DialogResult>> OnDialogRequested;

    private NPCInteractionHandler _npcInteractionHandler;

    protected override void Awake()
    {
        base.Awake();

        _npcInteractionHandler = _interactionHandler as NPCInteractionHandler;

        _npcInteractionHandler.onDialogRequested += NPC_onDialogRequested;
    }

    private void NPC_onDialogRequested(Character p_other, Dialog p_dialog, System.Action<DialogResult> p_resultAction)
    {
        bool __know = _npcInteractionHandler.know;
        OnDialogRequested?.Invoke(__know ? realName : genericName, p_other.realName, p_dialog, p_resultAction);
    }
}