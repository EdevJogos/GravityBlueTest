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

        guiManager.onItemPurchaseRequested += GuiManager_onItemPurchaseRequested;
        guiManager.onItemSellRequested += GuiManager_onItemSellRequested;
        guiManager.onShopCloseRequested += GuiManager_onShopCloseRequested;
        guiManager.onWearClothingRequested += GuiManager_onWearClothingRequested;
        guiManager.onExitWardrobeRequested += GuiManager_onExitWardrobeRequested;

        playerManager.onWardrobeRequested += PlayerManager_onWardrobeRequested;

        nPCManager.onStartDialogRequested += NPCManager_onStartDialogRequested;
        nPCManager.onDialogExitRequested += NPCManager_onDialogExitRequested;
        nPCManager.onShopRequested += NPCManager_onShopRequested;

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

    private void NPCManager_onStartDialogRequested(string p_requested, string p_requisitioner, Dialog p_dialog)
    {
        ChangeGameState(GameState.DIALOG);
        inputManager.SwitchInput(Inputs.DIALOG);
        guiManager.ShowDialog(p_requested, p_requisitioner, p_dialog);
    }

    private void NPCManager_onDialogExitRequested()
    {
        Debug.Log("NPCManager_onDialogExitRequested");
        FinishInteraction();
    }

    #endregion

    #region //-----------------INPUT MANAGER------------------

    private void DialogInputs_onSelectOptionRequested(Vector2 p_direction)
    {
        //Debug.Log("DialogInputs_onSelectOptionRequested " + p_direction);
        guiManager.UpdateSelectedChoice(p_direction);
    }

    private void DialogInputs_onConfirmRequested()
    {
        Debug.Log("DialogInputs_onConfirmRequested");
        nPCManager.OnDialogOptionConfirmed(guiManager.GetDialogResult());
    }

    private void NPCManager_onShopRequested(NPC p_npc, ShopData p_shopData)
    {
        Debug.Log("NPCManager_onShopRequested");
        ChangeGameState(GameState.SHOP);
        inputManager.SwitchInput(Inputs.SHOP);
        guiManager.UpdateDisplay(Displays.SHOP, ShopDisplay.CREATE_SHOP, new object[2] { p_shopData, playerManager.PlayerInventory });
        guiManager.ShowDisplay(Displays.SHOP);
    }

    #endregion

    #region//-----------------GUI MANAGER------------------

    private void GuiManager_onItemPurchaseRequested(ItemData p_itemData)
    {
        if(playerManager.CanPurchaseItem(p_itemData.purchaseValue))
        {
            playerManager.RemoveCash(p_itemData.purchaseValue);
            int __quantity = playerManager.PlayerInventory.AddToInventory(p_itemData, 1);
            guiManager.UpdateDisplay(Displays.SHOP, ShopDisplay.PURCHSE_CONFIRMED, __quantity);
        }
    }

    private void GuiManager_onItemSellRequested(ItemData p_itemData)
    {
        Debug.Log("GuiManager_onItemSellRequested");
        playerManager.AddCash(p_itemData.sellValue);
        int __quantity = playerManager.PlayerInventory.RemoveFromInventory(p_itemData, 1);
        guiManager.UpdateDisplay(Displays.SHOP, ShopDisplay.SELL_CONFIRMED, __quantity);
    }

    private void GuiManager_onShopCloseRequested()
    {
        FinishInteraction();
    }

    private void GuiManager_onWearClothingRequested(ClothData p_clothData)
    {
        playerManager.SwitchClothes(p_clothData);
        FinishInteraction();
    }

    private void GuiManager_onExitWardrobeRequested()
    {
        FinishInteraction();
    }

    #endregion

    //-----------------SCORE MANAGER----------------

    //-----------------STAGE MANAGER----------------

    #region//-----------------PLAYER MANAGER----------------

    private void PlayerManager_onWardrobeRequested()
    {
        inputManager.SwitchInput(Inputs.SHOP);
        guiManager.UpdateDisplay(Displays.WARDROBE, 0, playerManager.PlayerInventory);
        guiManager.ShowDisplay(Displays.WARDROBE);
    }

    #endregion
}
