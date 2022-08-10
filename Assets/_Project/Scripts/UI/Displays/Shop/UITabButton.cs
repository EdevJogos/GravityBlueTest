using UnityEngine;
using UnityEngine.UI;

public class UITabButton : MonoBehaviour
{
    public Sprite selected, unselected;

    public void Select(bool p_select)
    {
        GetComponent<Image>().sprite = p_select ? selected : unselected;
    }
}
