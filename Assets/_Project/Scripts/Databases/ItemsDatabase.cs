using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class ItemsDatabase : MonoBehaviour, IDatabase
{
    private static Dictionary<ItemsTypes, List<ItemData>> _IngredientsDatabase = new Dictionary<ItemsTypes, List<ItemData>>(2);

    public void Initiate()
    {
        foreach (ItemsTypes __itemType in System.Enum.GetValues(typeof(ItemsTypes)))
        {
            _IngredientsDatabase.Add(__itemType, new List<ItemData>(20));
        }

        Addressables.LoadAssetsAsync<IngredientData>("Ingredients", null).Completed += ItemsDatabase_LoadIngredientsCompleted;
    }

    private void ItemsDatabase_LoadIngredientsCompleted(AsyncOperationHandle<IList<IngredientData>> p_resultList)
    {
        foreach (var __ingredient in p_resultList.Result)
        {
            _IngredientsDatabase[ItemsTypes.INGREDIENT].Add(__ingredient);
        }

        Debug.Log("Ingredients Loaded");
    }

    public static List<ItemData> GetItemsOfType(ItemsTypes p_type)
    {
        return _IngredientsDatabase[p_type];
    }
}
