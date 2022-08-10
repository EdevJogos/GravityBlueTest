using System.Collections;
using UnityEngine;

public class Player : Character
{
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
        yield return new WaitForSeconds(1f);

        Debug.Log(ItemsDatabase.GetItemsOfType(ItemsTypes.INGREDIENT).Count);
        ItemData __itemData = ItemsDatabase.GetItemsOfType(ItemsTypes.INGREDIENT)[0];
        inventory.AddToInventory(__itemData, 5);
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
