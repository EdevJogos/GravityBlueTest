using System.Collections;
using UnityEngine;

public class NPCManager : MonoBehaviour
{
    public event System.Action<Dialog> onTalkRequested;
    public event System.Action<NPC, ShopData> onShopRequested;
    public event System.Action<NPC, Character, Dialog> onStartDialogRequested;
    public event System.Action onDialogExitRequested;

    public void Initiate()
    {
        NPC.OnTalkRequested += NPC_OnTalkRequested;
        NPC.OnDialogRequested += NPC_OnStartDialogRequested;
        NPC.OnExitDialogRequested += NPC_OnExitDialogRequested;
        NPC.OnShopRequested += NPC_OnShopRequested;
    }

    public void OnDialogOptionConfirmed(NPC p_requested, DialogResult p_result)
    {
        p_requested.NPCInteractionHandler.OnDialogResultRecieved(p_result);
    }

    public void FinishInteraction()
    {
        
    }

    private void NPC_OnTalkRequested(Dialog p_dialog)
    {
        onTalkRequested?.Invoke(p_dialog);
    }

    private void NPC_OnStartDialogRequested(NPC p_npc, Character p_other, Dialog p_dialog)
    {
        onStartDialogRequested?.Invoke(p_npc, p_other, p_dialog);
    }

    private void NPC_OnExitDialogRequested()
    {
        onDialogExitRequested?.Invoke();
    }

    private void NPC_OnShopRequested(NPC p_npc, ShopData p_shopData)
    {
        onShopRequested?.Invoke(p_npc, p_shopData);
    }
}