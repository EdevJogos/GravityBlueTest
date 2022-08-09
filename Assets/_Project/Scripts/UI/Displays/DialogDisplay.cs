using System.Collections;
using UnityEngine;

public class DialogDisplay : Display
{
    public DialogBox dialogBox;

    public void UpdateDialogBox(string p_name, string p_dialog)
    {
        dialogBox.nameText.text = p_name;
        dialogBox.dialogText.text = p_dialog;
    }
}