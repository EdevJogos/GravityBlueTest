using System.Collections;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public PlayerInventory PlayerInventory => player.inventory;

    public Player player;
}