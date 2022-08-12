using System.Collections;
using UnityEngine;

public class Player : Character
{
    public CharacterAnimationHandler AnimationHandler => GetComponent<CharacterAnimationHandler>();

    public PlayerInputsHandler playerInputHandler;

    [System.NonSerialized] public PlayerInventory inventory;

    protected override void Awake()
    {
        base.Awake();

        playerInputHandler.onMovementPerformed += PlayerInputHandler_onMovementPerformed;
        playerInputHandler.onInteractRequested += _playerInputHandler_onInteractRequested;

        inventory = new PlayerInventory();
    }

    

    protected override void Start()
    {
        base.Start();

        StartCoroutine(RoutineSetBasicItems());
    }

    private IEnumerator RoutineSetBasicItems()
    {
        while (!ItemsDatabase.IngredientsLoaded)
        {
            yield return null;
        }

        Debug.Log(ItemsDatabase.GetItemsOfType(ItemsTypes.INGREDIENTS).Count);
        ItemData __itemData = ItemsDatabase.GetItemsOfType(ItemsTypes.INGREDIENTS)[0];
        inventory.AddToInventory(__itemData, 5);

        while (!ItemsDatabase.ClothingLoaded)
        {
            yield return null;
        }

        __itemData = ItemsDatabase.GetItemsOfType(ItemsTypes.CLOTHINGS)[5];
        inventory.AddToInventory(__itemData, 1);
        __itemData = ItemsDatabase.GetItemsOfType(ItemsTypes.CLOTHINGS)[0];
        inventory.AddToInventory(__itemData, 1);
    }

    #region INPUT_HANDLER

    private void PlayerInputHandler_onMovementPerformed(Vector2 p_input)
    {
        _movementHandler.SetInput(p_input);
    }

    private void _playerInputHandler_onInteractRequested()
    {
        _interactionHandler.RequestInteraction();
    }

    #endregion
}
