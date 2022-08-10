using System.Collections;
using UnityEngine;

public class NPCManager : MonoBehaviour
{
    public event System.Action<NPC, ShopData> onShopRequested;
    public event System.Action<string, string, Dialog> onStartDialogRequested;

    private Character _requisitioner;
    private NPC _requested;

    public void Initiate()
    {
        NPC.OnDialogRequested += NPC_OnStartDialogRequested;
        NPC.OnShopRequested += NPC_OnShopRequested;
    }


    public void OnDialogOptionConfirmed(DialogResult p_result)
    {
        _requested.NPCInteractionHandler.OnDialogResultRecieved(p_result);
    }

    private void NPC_OnStartDialogRequested(NPC p_npc, Character p_other, Dialog p_dialog)
    {
        _requisitioner = p_other;
        _requested = p_npc;

        //Check if the NPC and the Player know each other, if so it will use its real name on the dialog box if not will use a generic name.
        bool __know = p_npc.NPCInteractionHandler.know;
        string __requestedName = _requested ? _requested.realName : _requested.genericName;
        string __requisitionerName = __know ? _requisitioner.realName : _requisitioner.genericName;

        onStartDialogRequested?.Invoke(__requestedName, __requisitionerName, p_dialog);
    }

    private void NPC_OnShopRequested(NPC p_npc, ShopData p_shopData)
    {
        onShopRequested?.Invoke(p_npc, p_shopData);
    }
}