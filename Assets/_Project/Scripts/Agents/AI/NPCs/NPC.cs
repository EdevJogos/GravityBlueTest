using UnityEngine;

public class NPC : Character
{
    public static event System.Action<NPC, ShopData> OnShopRequested;
    public static event System.Action<NPC, Character, Dialog> OnDialogRequested;

    public ShopData shopData;

    [System.NonSerialized] public NPCInteractionHandler NPCInteractionHandler;

    protected override void Awake()
    {
        base.Awake();

        NPCInteractionHandler = _interactionHandler as NPCInteractionHandler;

        NPCInteractionHandler.onDialogRequested += NPC_onDialogRequested;
        NPCInteractionHandler.onShopRequested += NPCInteractionHandler_onShopRequested;
    }

    private void NPC_onDialogRequested(Character p_other, Dialog p_dialog)
    {
        OnDialogRequested?.Invoke(this, p_other, p_dialog);
    }

    private void NPCInteractionHandler_onShopRequested()
    {
        OnShopRequested?.Invoke(this, shopData);
    }
}