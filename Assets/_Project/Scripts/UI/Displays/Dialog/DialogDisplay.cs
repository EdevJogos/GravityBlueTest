using System.Collections;
using UnityEngine;

public class DialogDisplay : Display
{
    public DialogBox dialogBox;
    public ChoiceBox choiceBox;

    public void UpdateDialogBox(NPC p_requested, Character p_requisitioner, string p_dialog)
    {
        //Check if the NPC and the Player know each other, if so it will use its real name on the dialog box if not will use a generic name.
        bool __know = p_requested.NPCInteractionHandler.know;
        string __nnk = __know ? p_requested.realName : p_requested.genericName;
        string __pnk = __know ? p_requested.NPCInteractionHandler.knownName : p_requisitioner.genericName;

        __pnk = __pnk.Replace("{pnr}", p_requisitioner.realName);

        dialogBox.nameText.text = __nnk;
        dialogBox.dialogText.text = p_dialog.Replace("{pnr}", p_requisitioner.realName).Replace("{nnr}", p_requested.realName).Replace("{pnk}", __pnk);
    }

    public void ShowChoicesBox(bool p_show)
    {
        choiceBox.Show(p_show);
    }

    public void UpdateChoicesBox(Character p_requisitioner, DialogChoice[] p_choices)
    {
        choiceBox.UpdateChoices(p_requisitioner, p_choices);
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