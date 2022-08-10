using System.Collections;
using UnityEngine;

public class ChoiceBox : MonoBehaviour
{
    public TMPro.TextMeshProUGUI nameText;
    public TMPro.TextMeshProUGUI[] choiceTexts;

    public void UpdateChoices(string p_name, DialogChoice[] p_choices)
    {
        nameText.text = p_name;

        for (int __i = 0; __i < choiceTexts.Length; __i++)
        {
            choiceTexts[__i].text = p_choices[__i].choiceText;
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