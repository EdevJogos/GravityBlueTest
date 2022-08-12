using System.Collections;
using UnityEngine;

public class ChoiceBox : MonoBehaviour
{
    public TMPro.TextMeshProUGUI nameText;
    public TMPro.TextMeshProUGUI[] choiceTexts;

    public void Show(bool p_show)
    {
        gameObject.SetActive(p_show);
    }

    public void UpdateChoices(Character p_requisitioner, DialogChoice[] p_choices)
    {
        int __i = 0;

        for (; __i < p_choices.Length; __i++)
        {
            choiceTexts[__i].text = p_choices[__i].choiceText.Replace("{pnr}", p_requisitioner.realName);
        }

        for (; __i < choiceTexts.Length; __i++)
        {
            choiceTexts[__i].text = "";
        }
    }

    public void SelectChoice(int p_index)
    {
        choiceTexts[p_index].text = "> " + choiceTexts[p_index].text;
    }

    public void UnselectChoice(int p_index)
    {
        choiceTexts[p_index].text = choiceTexts[p_index].text.Remove(0, 2);
    }
}