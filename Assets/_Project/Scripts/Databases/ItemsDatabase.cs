using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class ItemsDatabase : MonoBehaviour, IDatabase
{
    public static bool ClothingLoaded = false, IngredientsLoaded = false;

    private static Dictionary<ItemsTypes, List<ItemData>> _ItemsDatabase = new Dictionary<ItemsTypes, List<ItemData>>(2);

    public void Initiate()
    {
        foreach (ItemsTypes __itemType in System.Enum.GetValues(typeof(ItemsTypes)))
        {
            _ItemsDatabase.Add(__itemType, new List<ItemData>(20));
        }

        Addressables.LoadAssetsAsync<ClothData>("CLOTHINGS", null).Completed += ItemsDatabase_Completed;
        Addressables.LoadAssetsAsync<IngredientData>("INGREDIENTS", null).Completed += ItemsDatabase_LoadIngredientsCompleted;
    }

    private void ItemsDatabase_Completed(AsyncOperationHandle<IList<ClothData>> p_resultList)
    {
        foreach (var __cloth in p_resultList.Result)
        {
            _ItemsDatabase[ItemsTypes.CLOTHINGS].Add(__cloth);
        }

        ClothingLoaded = true;
    }

    private void ItemsDatabase_LoadIngredientsCompleted(AsyncOperationHandle<IList<IngredientData>> p_resultList)
    {
        foreach (var __ingredient in p_resultList.Result)
        {
            _ItemsDatabase[ItemsTypes.INGREDIENTS].Add(__ingredient);
        }

        IngredientsLoaded = true;
    }

    public static List<ItemData> GetItemsOfType(ItemsTypes p_type)
    {
        return _ItemsDatabase[p_type];
    }
}
