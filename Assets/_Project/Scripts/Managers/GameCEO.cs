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

        nPCManager.onStartDialogRequested += NPCManager_onStartDialogRequested;
        nPCManager.onShopRequested += NPCManager_onShopRequested;

        cameraManager.Initiate();
        audioManager.Initate();
        guiManager.Initiate();
        inputManager.Initiate();
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

    #region //-----------------NPC MANAGER------------------

    private void NPCManager_onStartDialogRequested(string p_requested, string p_requisitioner, Dialog p_dialog)
    {
        ChangeGameState(GameState.DIALOG);
        inputManager.SwitchInput(Inputs.DIALOG);
        guiManager.ShowDialog(p_requested, p_requisitioner, p_dialog);
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
        guiManager.UpdateDisplay(Displays.SHOP, 0, new object[2] { p_shopData, playerManager.PlayerInventory });
        guiManager.ShowDisplay(Displays.SHOP);
    }

    #endregion
    //-----------------GUI MANAGER------------------

    //-----------------SCORE MANAGER----------------

    //-----------------STAGE MANAGER----------------

    //-----------------AGENTS MANAGER----------------
}
