using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory
{
    public class StockedItem
    {
        public int quantity;
        public ItemData data;

        public StockedItem(int p_quantity, ItemData p_data)
        {
            quantity = p_quantity;
            data = p_data;
        }
    }

    public Dictionary<ItemsTypes, List<StockedItem>> itemsIventory = new Dictionary<ItemsTypes, List<StockedItem>>(4);

    public void AddToInventory(ItemData p_data, int p_quantity)
    {
        if(!itemsIventory.ContainsKey(p_data.itemType))
        {
            itemsIventory.Add(p_data.itemType, new List<StockedItem>(10));
        }

        StockedItem __stockedItem = GetStockedItem(p_data);

        if(__stockedItem == null)
        {
            itemsIventory[p_data.itemType].Add(new StockedItem(p_quantity, p_data));
        }
        else
        {
            __stockedItem.quantity += p_quantity;
        }
    }

    public StockedItem GetStockedItem(ItemData p_data)
    {
        if (!itemsIventory.ContainsKey(p_data.itemType))
            return null;

        List<StockedItem> __stockedITems = itemsIventory[p_data.itemType];

        for (int __i = 0; __i < __stockedITems.Count; __i++)
        {
            if (__stockedITems[__i].data == p_data)
            {
                return __stockedITems[__i];
            }
        }

        return null;
    }

    public List<StockedItem> GetStockedItemsOfType(ItemsTypes p_type)
    {
        return itemsIventory[p_type];
    }
}