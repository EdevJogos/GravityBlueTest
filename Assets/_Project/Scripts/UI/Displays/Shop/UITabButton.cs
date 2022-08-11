using UnityEngine;
using UnityEngine.UI;

public class UITabButton : MonoBehaviour
{
    public Sprite selected, unselected;

    public void SetVisual(Sprite p_normal, Sprite p_selected)
    {
        GetComponentInChildren<Image>().sprite = p_normal;
        selected = p_selected;
        unselected = p_normal;
    }

    public void Select(bool p_select)
    {
        GetComponent<Image>().sprite = p_select ? selected : unselected;
    }
}
