using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameCEO : MonoBehaviour
{
    private static GameCEO Instance;

    public static event System.Action onGameStateChanged;

    public static bool Tutorial { get; private set; } = true;
    public static GameState State { get; private set; }

    #if UNITY_EDITOR
    public GameState state;
    #endif


    public GUIManager guiManager;
    public InputManager inputManager;
    public CameraManager cameraManager;
    public AudioManager audioManager;
    public PlayerManager playerManager;
    public StageManager stageManager;
    public NPCManager nPCManager;

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
            Initiate();
        }
    }

    private void Start()
    {
        Initialize();
    }

    private void Initiate()
    {
        inputManager.dialogInputs.onConfirmRequested += DialogInputs_onConfirmRequested;
        inputManager.dialogInputs.onSelectOptionRequested += DialogInputs_onSelectOptionRequested;

        guiManager.onDialogOptionSelected += GuiManager_onDialogOptionSelected;
        guiManager.onItemPurchaseRequested += GuiManager_onItemPurchaseRequested;
        guiManager.onItemSellRequested += GuiManager_onItemSellRequested;
        guiManager.onShopCloseRequested += GuiManager_onShopCloseRequested;
        guiManager.onWearClothingRequested += GuiManager_onWearClothingRequested;
        guiManager.onExitWardrobeRequested += GuiManager_onExitWardrobeRequested;

        playerManager.onCashAltered += PlayerManager_onCashAltered;
        playerManager.onWardrobeRequested += PlayerManager_onWardrobeRequested;

        nPCManager.onStartDialogRequested += NPCManager_onStartDialogRequested;
        nPCManager.onTalkRequested += NPCManager_onTalkRequested;
        nPCManager.onDialogExitRequested += NPCManager_onDialogExitRequested;
        nPCManager.onShopRequested += NPCManager_onShopRequested;

        stageManager.onBreadSold += StageManager_onBreadSold;

        cameraManager.Initiate();
        audioManager.Initate();
        guiManager.Initiate();
        inputManager.Initiate();
        playerManager.Initiate();
        nPCManager.Initiate();
    }

    public void Initialize()
    {
        audioManager.Initialize();
        inputManager.Initialize();
        guiManager.Initialize();

        guiManager.ShowDisplay(Displays.HUD);
        ChangeGameState(GameState.PLAY);
    }

    //-----------------CEO------------------

    private void ChangeGameState(GameState p_state)
    {
        #if UNITY_EDITOR
            state = p_state;
        #endif

        State = p_state;
        onGameStateChanged?.Invoke();
    }

    private IEnumerator RoutineLoadScene(int p_scene, float p_delay = 0f)
    {
        if (p_delay > 0) yield return new WaitForSeconds(p_delay);

        var sceneLoader = SceneManager.LoadSceneAsync(p_scene);

        while (sceneLoader.progress <= 1)
        {
            yield return null;
        }
    }

    private void FinishInteraction()
    {
        nPCManager.FinishInteraction();
        guiManager.FinishDialog();
        guiManager.ShowDisplay(Displays.HUD);

        inputManager.SwitchInput(Inputs.PLAYER);
        ChangeGameState(GameState.PLAY);
    }

    #region //-----------------NPC MANAGER------------------

    private void NPCManager_onStartDialogRequested(NPC p_requested, Character p_requisitioner, Dialog p_dialog)
    {
        ChangeGameState(GameState.DIALOG);
        inputManager.SwitchInput(Inputs.DIALOG);
        guiManager.StartDialog(p_requested, p_requisitioner, p_dialog);
    }

    private void NPCManager_onTalkRequested(Dialog p_dialog)
    {
        guiManager.StartTalk(p_dialog);
    }

    private void NPCManager_onDialogExitRequested()
    {
        FinishInteraction();
    }

    #endregion

    #region //-----------------INPUT MANAGER------------------

    private void DialogInputs_onSelectOptionRequested(Vector2 p_direction)
    {
        AudioManager.PlaySFX(SFXOccurrence.OPTION_SELECTED);
        guiManager.UpdateSelectedChoice(p_direction);
    }

    private void DialogInputs_onConfirmRequested()
    {
        AudioManager.PlaySFX(SFXOccurrence.OPTION_CONFIRMED);
        guiManager.UpdateDialog();
    }

    private void NPCManager_onShopRequested(NPC p_npc, ShopData p_shopData)
    {
        ChangeGameState(GameState.SHOP);
        inputManager.SwitchInput(Inputs.SHOP);
        guiManager.UpdateDisplay(Displays.SHOP, ShopDisplay.CREATE_SHOP, new object[2] { p_shopData, playerManager.PlayerInventory });
        guiManager.ShowDisplay(Displays.SHOP);
    }

    #endregion

    #region//-----------------GUI MANAGER------------------

    private void GuiManager_onDialogOptionSelected(NPC p_requested, DialogResult p_result)
    {
        AudioManager.PlaySFX(SFXOccurrence.OPTION_CONFIRMED);
        nPCManager.OnDialogOptionConfirmed(p_requested, p_result);
    }

    private void GuiManager_onItemPurchaseRequested(ItemData p_itemData)
    {
        if(playerManager.CanPurchaseItem(p_itemData.purchaseValue))
        {
            playerManager.RemoveCash(p_itemData.purchaseValue);
            int __quantity = playerManager.PlayerInventory.AddToInventory(p_itemData, 1);
            guiManager.UpdateDisplay(Displays.SHOP, ShopDisplay.PURCHSE_CONFIRMED, __quantity);
            AudioManager.PlaySFX(SFXOccurrence.BUY);
        }
        else
        {
            AudioManager.PlaySFX(SFXOccurrence.OPTION_DENIED);
        }
    }

    private void GuiManager_onItemSellRequested(ItemData p_itemData)
    {
        playerManager.AddCash(p_itemData.sellValue);
        int __quantity = playerManager.PlayerInventory.RemoveFromInventory(p_itemData, 1);
        guiManager.UpdateDisplay(Displays.SHOP, ShopDisplay.SELL_CONFIRMED, __quantity);
        AudioManager.PlaySFX(SFXOccurrence.SELL);
    }

    private void GuiManager_onShopCloseRequested()
    {
        AudioManager.PlaySFX(SFXOccurrence.CLOSE_PANEL);
        FinishInteraction();
    }

    private void GuiManager_onWearClothingRequested(ClothData p_clothData)
    {
        AudioManager.PlaySFX(SFXOccurrence.WEAR);
        playerManager.SwitchClothes(p_clothData);
        FinishInteraction();
    }

    private void GuiManager_onExitWardrobeRequested()
    {
        AudioManager.PlaySFX(SFXOccurrence.CLOSE_PANEL);
        FinishInteraction();
    }

    #endregion

    //-----------------SCORE MANAGER----------------

    #region//-----------------STAGE MANAGER----------------

    private void StageManager_onBreadSold()
    {
        playerManager.AddCash(500);
        AudioManager.PlaySFX(SFXOccurrence.BUY);
    }

    #endregion

    #region//-----------------PLAYER MANAGER----------------

    private void PlayerManager_onCashAltered(int p_cash)
    {
        guiManager.UpdateDisplay(Displays.HUD, HUDDisplay.UPDATE_PLAYER_CASH, playerManager.PlayerCash);
    }

    private void PlayerManager_onWardrobeRequested()
    {
        inputManager.SwitchInput(Inputs.SHOP);
        guiManager.UpdateDisplay(Displays.WARDROBE, 0, playerManager.PlayerInventory);
        guiManager.ShowDisplay(Displays.WARDROBE);
    }

    #endregion
}
