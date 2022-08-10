using UnityEngine;
using UnityEngine.UI;

public class ShopItemBar : MonoBehaviour
{
    public Image icon;
    public TMPro.TextMeshProUGUI nameText;
    public TMPro.TextMeshProUGUI valueText;
    public TMPro.TextMeshProUGUI qtText;
    public Button button;

    public void Initialize(ItemData p_data, int p_quantity, bool p_purchase, System.Action<ItemData> p_pressed)
    {
        icon.sprite = p_data.icon;
        nameText.text = p_data.name;
        qtText.text = "x" + p_quantity;
        valueText.text = (p_purchase ? p_data.purchaseValue : p_data.sellValue) + "";
        button.onClick.AddListener(() => p_pressed.Invoke(p_data));
    }
}