using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopDisplay : Display
{
    public const int CREATE_SHOP = 0;
    public const int PURCHSE_CONFIRMED = 0;
    public const int SELL_CONFIRMED = 1;

    public UITabButton purchaseButton, sellButton;
    public Image panelImage;
    public TMPro.TextMeshProUGUI playerCashText;
    public Transform purchaseGrid, sellGrid;

    private SpriteState _spriteState;
    private ShopData _curShopData;
    private PlayerInventory _requisiterInventory;
    private ShopItemBar _interactedBar;
    
    private List<ShopItemBar> _purchaseBars = new List<ShopItemBar>(10);
    private List<ShopItemBar> _sellBars = new List<ShopItemBar>(10);

    public override void Show(bool p_show, Action p_callback, float p_ratio)
    {
        if(!p_show)
        {
            _requisiterInventory = null;
            _interactedBar = null;

            for (int __i = _purchaseBars.Count - 1; __i >= 0; __i--)
            {
                Destroy(_purchaseBars[__i].gameObject);
            }

            for (int __i = _sellBars.Count - 1; __i >= 0; __i--)
            {
                Destroy(_sellBars[__i].gameObject);
            }

            _purchaseBars.Clear();
            _sellBars.Clear();
        }
        else
        {
            ShowPurchaseTab();
        }

        base.Show(p_show, p_callback, p_ratio);
    }

    public override void UpdateDisplay(int p_operation, float p_value, float p_data)
    {
        switch(p_operation)
        {
            case PURCHSE_CONFIRMED:
                PurchaseConfirmed(p_value);
                break;
            case SELL_CONFIRMED:
                SellConfirmed(p_value);
                break;
        }
    }

    public override void UpdateDisplay(int p_operation, object p_data)
    {
        switch (p_operation)
        {
            case CREATE_SHOP:
                object[] __datas = p_data as object[];
                ShopData __shopData = (ShopData)__datas[0];
                PlayerInventory __inventory = __datas[1] as PlayerInventory;
                CreateShop(__shopData, __inventory);
                break;
        }
    }

    private void CreateShop(ShopData p_shopData, PlayerInventory p_inventory)
    {
        _curShopData = p_shopData;
        _requisiterInventory = p_inventory;

        SetVisual(p_shopData);

        List<ItemData> __items = ItemsDatabase.GetItemsOfType(p_shopData.itemType);

        for (int __i = 0; __i < __items.Count; __i++)
        {
            ShopItemBar __shopItemBar = PrefabsDatabase.InstantiatePrefab<ShopItemBar>(Prefabs.SHOP_ITEM_BAR, 0, purchaseGrid);

            PlayerInventory.StockedItem __stockedItem = p_inventory.GetStockedItem(__items[__i]);
            int __playerOwns = __stockedItem == null ? 0 : __stockedItem.quantity;

            __shopItemBar.Initialize(_curShopData.itemBarSprite, _curShopData.buttonNormalSprite, _spriteState, __items[__i], __playerOwns, true, ButtonPurchasePressed);

            _purchaseBars.Add(__shopItemBar);
        }

        UpdateSellTab();
    }

    private void SetVisual(ShopData p_shopData)
    {
        panelImage.sprite = p_shopData.panelSprite;

        _spriteState = new SpriteState();
        _spriteState.selectedSprite = p_shopData.buttonPressedSprite;
        _spriteState.pressedSprite = p_shopData.buttonPressedSprite;
        _spriteState.highlightedSprite = p_shopData.buttonPressedSprite;

        purchaseButton.SetVisual(p_shopData.buttonNormalSprite, p_shopData.buttonPressedSprite);
        sellButton.SetVisual(p_shopData.buttonNormalSprite, p_shopData.buttonPressedSprite);
    }
        

    private void PurchaseConfirmed(float p_quantity)
    {
        _interactedBar.UpdateItemQuantity((int)p_quantity);
        playerCashText.text = _requisiterInventory.cash.ToString();
    }

    private void SellConfirmed(float p_quantity)
    {
        if(p_quantity == 0)
        {
            _sellBars.Remove(_interactedBar);
            Destroy(_interactedBar.gameObject);
        }
        else
        {
            _interactedBar.UpdateItemQuantity((int)p_quantity);
        }

        playerCashText.text = _requisiterInventory.cash.ToString();
    }

    private void ButtonPurchasePressed(ItemData p_itemData, ShopItemBar p_bar)
    {
        _interactedBar = p_bar;

        RequestAction(0, p_itemData);
    }

    private void ButtonSellPressed(ItemData p_itemData, ShopItemBar p_bar)
    {
        _interactedBar = p_bar;

        RequestAction(1, p_itemData);
    }

    private void UpdatePurchaseTab()
    {
        for (int __i = 0; __i < _purchaseBars.Count; __i++)
        {
            PlayerInventory.StockedItem __stockedItem = _requisiterInventory.GetStockedItem(_purchaseBars[__i].ItemData);
            int __playerOwns = __stockedItem == null ? 0 : __stockedItem.quantity;

            _purchaseBars[__i].UpdateItemQuantity(__playerOwns);
        }
    }

    private void UpdateSellTab()
    {
        List<PlayerInventory.StockedItem> __stockedItems = _requisiterInventory.GetStockedItemsOfType(_curShopData.itemType);

        if (__stockedItems == null)
            return;

        int __i = 0;

        for (; __i < _sellBars.Count; __i++)
        {
            if (__stockedItems.Count == __i)
                return;

            _sellBars[__i].Initialize(_curShopData.itemBarSprite, _curShopData.buttonNormalSprite, _spriteState, __stockedItems[__i].data, __stockedItems[__i].quantity, false, ButtonSellPressed);
        }

        for (; __i < __stockedItems.Count; __i++)
        {
            ShopItemBar __shopItemBar = PrefabsDatabase.InstantiatePrefab<ShopItemBar>(Prefabs.SHOP_ITEM_BAR, 0, sellGrid);

            __shopItemBar.Initialize(_curShopData.itemBarSprite, _curShopData.buttonNormalSprite, _spriteState, __stockedItems[__i].data, __stockedItems[__i].quantity, false, ButtonSellPressed);

            _sellBars.Add(__shopItemBar);
        }
    }

    private void ShowPurchaseTab()
    {
        purchaseButton.Select(true);
        sellGrid.gameObject.SetActive(false);
        purchaseGrid.gameObject.SetActive(true);
    }

    public override void RequestAction(int p_action)
    {
        switch (p_action)
        {
            case 0:
                UpdatePurchaseTab();
                ShowPurchaseTab();
                break;
            case 1:
                UpdateSellTab();

                purchaseGrid.gameObject.SetActive(false);
                sellGrid.gameObject.SetActive(true);
                break;
            case 2:
                RequestAction(2, null);
                break;
        }
    }
}
