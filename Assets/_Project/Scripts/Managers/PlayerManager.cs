using System.Collections;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public PlayerInventory PlayerInventory => player.inventory;

    public Player player;

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
}