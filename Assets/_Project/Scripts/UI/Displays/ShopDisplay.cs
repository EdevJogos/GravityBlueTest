using System.Collections.Generic;
using UnityEngine;

public class ShopDisplay : Display
{
    public Transform purchaseGrid, sellGrid;
    public override void UpdateDisplay(int p_operation, object p_data)
    {
        switch (p_operation)
        {
            case 0:
                object[] __datas = p_data as object[];
                ShopData __shopData = (ShopData)__datas[0];
                PlayerInventory __inventory = __datas[1] as PlayerInventory;
                UpdateShop(__shopData, __inventory);
                break;
        }
    }

    private void UpdateShop(ShopData p_shopData, PlayerInventory p_inventory)
    {
        List<ItemData> __items = ItemsDatabase.GetItemsOfType(p_shopData.itemType);

        for (int __i = 0; __i < __items.Count; __i++)
        {
            ShopItemBar __shopItemBar = PrefabsDatabase.InstantiatePrefab<ShopItemBar>(Prefabs.SHOP_ITEM_BAR, 0, purchaseGrid);

            PlayerInventory.StockedItem __stockedItem = p_inventory.GetStockedItem(__items[__i]);
            int __playerOwns = __stockedItem == null ? 0 : __stockedItem.quantity;

            __shopItemBar.Initialize(__items[__i], __playerOwns, true, ButtonPurchasePressed);
        }

        List<PlayerInventory.StockedItem> __stockedItems = p_inventory.GetStockedItemsOfType(p_shopData.itemType);

        for (int __i = 0; __i < __stockedItems.Count; __i++)
        {
            ShopItemBar __shopItemBar = PrefabsDatabase.InstantiatePrefab<ShopItemBar>(Prefabs.SHOP_ITEM_BAR, 0, sellGrid);

            __shopItemBar.Initialize(__stockedItems[__i].data, __stockedItems[__i].quantity, false, ButtonSellPressed);
        }
    }

    private void ButtonPurchasePressed(ItemData p_itemData)
    {
        Debug.Log("ButtonPurchasePressed " + p_itemData.itemName);
    }

    private void ButtonSellPressed(ItemData p_itemData)
    {
        Debug.Log("ButtonSellPressed " + p_itemData.itemName);
    }

    public override void RequestAction(int p_action)
    {
        switch (p_action)
        {
            case 0:
                sellGrid.gameObject.SetActive(false);
                purchaseGrid.gameObject.SetActive(true);
                break;
            case 1:

                purchaseGrid.gameObject.SetActive(false);
                sellGrid.gameObject.SetActive(true);
                break;
        }
    }
}
