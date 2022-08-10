using System.Collections;
using UnityEngine;

public class DialogDisplay : Display
{
    public DialogBox dialogBox;
    public ChoiceBox choiceBox;

    public void UpdateDialogBox(string p_name, string p_dialog)
    {
        dialogBox.nameText.text = p_name;
        dialogBox.dialogText.text = p_dialog;
    }

    public void UpdateChoicesBox(string p_name, DialogChoice[] p_choices)
    {
        choiceBox.UpdateChoices(p_name, p_choices);
    }

    public void SelectChoice(int p_index)
    {
        choiceBox.SelectChoice(p_index);
    }

    public void UnselectChoice(int p_index)
    {
        choiceBox.UnselectChoice(p_index);
    }
}