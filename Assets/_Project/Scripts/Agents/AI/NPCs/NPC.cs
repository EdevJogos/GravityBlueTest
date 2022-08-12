using UnityEngine;

public class NPC : Character
{
    public static event System.Action<NPC, ShopData> OnShopRequested;
    public static event System.Action<NPC, Character, Dialog> OnDialogRequested;
    public static event System.Action<Dialog> OnTalkRequested;
    public static event System.Action OnExitDialogRequested;

    public bool IsActive => gameObject.activeSelf;

    public ShopData shopData;

    [System.NonSerialized] public NPCInteractionHandler NPCInteractionHandler;

    private NPCBehaviour _behaviour;

    protected override void Awake()
    {
        base.Awake();

        _behaviour = GetComponent<NPCBehaviour>();

        NPCInteractionHandler = _interactionHandler as NPCInteractionHandler;

        NPCInteractionHandler.onDialogRequested += NPC_onDialogRequested;
        NPCInteractionHandler.onTalkRequested += NPCInteractionHandler_onTalkRequested;
        NPCInteractionHandler.onExitDialogRequested += NPCInteractionHandler_onExitDialogRequested;
        NPCInteractionHandler.onShopRequested += NPCInteractionHandler_onShopRequested;

        _behaviour?.Initiate();
    }

    protected override void Update()
    {
        base.Update();

        if (_behaviour == null)
            return;

        _behaviour.Tick();
        _movementHandler.SetInput(_behaviour.input);
    }

    protected override void FixedUpdate()
    {
        _movementHandler.FixedTick();
    }

    public void Active(bool p_active)
    {
        gameObject.SetActive(p_active);
    }

    private void NPCInteractionHandler_onTalkRequested(Dialog p_dialog)
    {
        OnTalkRequested?.Invoke(p_dialog);
    }

    private void NPC_onDialogRequested(Character p_other, Dialog p_dialog)
    {
        OnDialogRequested?.Invoke(this, p_other, p_dialog);
    }

    private void NPCInteractionHandler_onExitDialogRequested()
    {
        OnExitDialogRequested?.Invoke();
    }

    private void NPCInteractionHandler_onShopRequested()
    {
        OnShopRequested?.Invoke(this, shopData);
    }
}