using UnityEngine;

[System.Serializable]
public class ItemData : ScriptableObject
{
    public ItemsTypes itemType;
    public string itemName;
    public Sprite icon;
    public int purchaseValue;
    public int sellValue;
}