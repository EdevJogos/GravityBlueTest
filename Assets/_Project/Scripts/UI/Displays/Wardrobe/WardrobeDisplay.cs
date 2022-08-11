using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WardrobeDisplay : Display
{
    public const int CREATE_WARDROBE = 0;

    public Button exitButton;
    public Transform grid;

    private List<WardrobeItemBar> _wardrobeBars = new List<WardrobeItemBar>(10);

    public override void Initiate()
    {
        base.Initiate();

        exitButton.onClick.AddListener(() => { RequestAction(1, null); });
    }

    public override void Show(bool p_show, Action p_callback, float p_ratio)
    {
        if (!p_show)
        {
            for (int __i = _wardrobeBars.Count - 1; __i >= 0; __i--)
            {
                Destroy(_wardrobeBars[__i].gameObject);
            }

            _wardrobeBars.Clear();
        }

        base.Show(p_show, p_callback, p_ratio);
    }

    public override void UpdateDisplay(int p_operation, object p_data)
    {
        switch (p_operation)
        {
            case CREATE_WARDROBE:
                PlayerInventory __inventory = p_data as PlayerInventory;
                CreateWardrobe(__inventory);
                break;
        }
    }

    private void CreateWardrobe(PlayerInventory p_inventory)
    {
        List<PlayerInventory.StockedItem> __stockedItems = p_inventory.GetStockedItemsOfType(ItemsTypes.CLOTHINGS);

        if (__stockedItems == null || __stockedItems.Count == 0)
            return;

        for (int __i = 0; __i < __stockedItems.Count; __i++)
        {
            WardrobeItemBar __wardrobeItemBar = PrefabsDatabase.InstantiatePrefab<WardrobeItemBar>(Prefabs.WARDROBE_ITEM_BAR, 0, grid);

            __wardrobeItemBar.Initialize(__stockedItems[__i].data, __stockedItems[__i].quantity, OnUseRequested);

            _wardrobeBars.Add(__wardrobeItemBar);
        }
    }

    private void OnUseRequested(ItemData p_data)
    {
        RequestAction(0, p_data);
    }
}
