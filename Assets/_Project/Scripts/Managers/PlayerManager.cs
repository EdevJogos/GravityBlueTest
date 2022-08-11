﻿using System.Collections;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public event System.Action onWardrobeRequested;

    public PlayerInventory PlayerInventory => player.inventory;

    public Player player;
    public Wardrobe wardrobe;

    public void Initiate()
    {
        wardrobe.onWardrobeRequested += Wardrobe_onWardrobeRequested;
    }

    public bool CanPurchaseItem(int p_cost)
    {
        return PlayerInventory.cash >= p_cost;
    }

    public void AddCash(int p_amount)
    {
        PlayerInventory.cash += p_amount;
    }

    public void RemoveCash(int p_amount)
    {
        PlayerInventory.cash -= p_amount;
    }

    public void SwitchClothes(ClothData p_data)
    {
        player.AnimationHandler.clothData = p_data;
    }

    private void Wardrobe_onWardrobeRequested(Wardrobe p_wardrobe)
    {
        onWardrobeRequested?.Invoke();
    }
}