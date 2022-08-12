using UnityEngine;
using UnityEngine.UI;

public class HUDDisplay : Display
{
    public const int UPDATE_PLAYER_CASH = 0;

    public TMPro.TextMeshProUGUI playerCashText;

    public override void UpdateDisplay(int p_operation, float p_value, float p_data)
    {
        switch (p_operation)
        {
            case UPDATE_PLAYER_CASH:
                playerCashText.text = p_value + "";
                break;
        }
    }
}
