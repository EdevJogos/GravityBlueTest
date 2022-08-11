using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class WardrobeItemBar : MonoBehaviour
{
    public ItemData ItemData { get; private set; }

    public Image icon;
    public TMPro.TextMeshProUGUI nameText;
    public TMPro.TextMeshProUGUI qtText;
    public Button button;

    public void Initialize(ItemData p_data, int p_quantity, System.Action<ItemData> p_pressed)
    {
        ItemData = p_data;

        icon.sprite = p_data.icon;
        nameText.text = p_data.itemName;
        qtText.text = "x" + p_quantity;

        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() => p_pressed.Invoke(p_data));
    }

    public void UpdateItemQuantity(int p_quantity)
    {
        qtText.text = "x" + p_quantity;
    }
}