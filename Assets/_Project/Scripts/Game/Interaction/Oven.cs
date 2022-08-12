using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oven : MonoBehaviour, IInteract
{
    public Interacter interacter;
    public BreadTable[] breadTables;

    private int _tableIndex = 0;
    private List<ItemData> _requiredIngredients;

    private void Awake()
    {
        interacter.ExecuteAction = ExecuteAction;
    }

    private IEnumerator Start()
    {
        while (!ItemsDatabase.IngredientsLoaded)
        {
            yield return null;
        }

        _requiredIngredients = ItemsDatabase.GetItemsOfType(ItemsTypes.INGREDIENTS);
    }

    public bool CanInteract => true;

    public Interactions GetInteraction()
    {
        return interacter.interaction;
    }

    public Interacter Interact()
    {
        return interacter;
    }

    private void ExecuteAction(Character p_character)
    {
        if (!HasIngredients(p_character))
        {
            AudioManager.PlaySFX(SFXOccurrence.OPTION_DENIED);
            return;
        }

        AudioManager.PlaySFX(SFXOccurrence.OVEN);

        ConsumeItems(p_character);

        breadTables[_tableIndex].AddBread();
        _tableIndex = HelpExtensions.ClampCircle(_tableIndex + 1, 0, breadTables.Length - 1);
    }

    private bool HasIngredients(Character p_character)
    {
        Player __player = p_character as Player;

        for (int __i = 0; __i < _requiredIngredients.Count; __i++)
        {
            PlayerInventory.StockedItem __stockedItem = __player.inventory.GetStockedItem(_requiredIngredients[__i]);
            
            if(__stockedItem == null)
            {
                return false;
            }
        }

        return true;
    }

    private void ConsumeItems(Character p_character)
    {
        Player __player = p_character as Player;

        for (int __i = 0; __i < _requiredIngredients.Count; __i++)
        {
            __player.inventory.RemoveFromInventory(_requiredIngredients[__i], 1);
        }
    }
}
