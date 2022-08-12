using UnityEngine;
using System;
using System.Collections.Generic;

public class GUIManager : MonoBehaviour
{
    public event Action<NPC, DialogResult> onDialogOptionSelected;

    public event Action<ItemData> onItemPurchaseRequested;
    public event Action<ItemData> onItemSellRequested;
    public event Action onShopCloseRequested;
    public event Action<ClothData> onWearClothingRequested;
    public event Action onExitWardrobeRequested;

    public Transform displaysHolder;

    private int _dialogIndex = 0;
    private int _choiceIndex = 0;
    private Dialog _curDialog;
    private DialogChoice[] _curChoices;
    [System.NonSerialized] public Character requisitioner;
    [System.NonSerialized] public NPC requested;

    private Display _activeDisplay;
    private Dictionary<Displays, Display> _displays = new Dictionary<Displays, Display>();

    public void Initiate()
    {
        Display.onActionRequested += OnActionRequested;
        Display.onDataActionRequested += OnDataActionRequested;

        foreach (Transform __transform in displaysHolder)
        {
            Display __display = __transform.GetComponent<Display>();

            if (__display == null)
                return;

            __display.Initiate();
            _displays.Add(__display.ID, __display);
        }
    }

    private void OnDestroy()
    {
        Display.onActionRequested -= OnActionRequested;
    }

    public void Initialize()
    {
        foreach (Display __display in _displays.Values)
        {
            __display.Initialize();
        }
    }

    public void ShowDisplay(Displays p_display, Action p_onShowCompleted = null, float p_hideRatio = 1f, float p_showRatio = 1f)
    {
        if (_activeDisplay == null || (_activeDisplay != null && _activeDisplay.ID != p_display))
        {
            if (_activeDisplay != null)
            {
                _activeDisplay.Show(false, () => { ActiveDisplay(p_display, p_onShowCompleted, p_showRatio); }, p_hideRatio);
            }
            else
            {
                ActiveDisplay(p_display, p_onShowCompleted, p_showRatio);
            }
        }
    }

    public void StartDialog(NPC p_requested, Character p_requisitioner, Dialog p_dialog)
    {
        _curDialog = p_dialog;
        requested = p_requested;
        requisitioner = p_requisitioner;

        UpdateDialog();

        ShowDisplay(Displays.DIALOG);
    }

    public void StartTalk(Dialog p_dialog)
    {
        _dialogIndex = 0;
        _curDialog = p_dialog;

        UpdateDialog();
    }

    public void UpdateDialog()
    {
        if (_curDialog.dialogLines.Length > _dialogIndex)
        {
            DialogDisplay __dialogDisplay = _displays[Displays.DIALOG] as DialogDisplay;

            __dialogDisplay.ShowChoicesBox(false);
            __dialogDisplay.UpdateDialogBox(requested, requisitioner, _curDialog.dialogLines[_dialogIndex].textLine);

            _dialogIndex++;

            if (_curDialog.dialogLines.Length == _dialogIndex)
            {
                if (_curDialog.choices.Length > 0)
                {
                    __dialogDisplay.ShowChoicesBox(true);

                    _curChoices = _curDialog.choices;
                    __dialogDisplay.UpdateChoicesBox(requisitioner, _curDialog.choices);
                    __dialogDisplay.SelectChoice(0);
                }
            }
        }
        else
        {
            DialogResult __result = _curDialog.choices.Length > 0 ? new DialogResult(_curChoices[_choiceIndex].id, _curChoices[_choiceIndex].data) : new DialogResult(DialogActions.EXIT, 0);
            onDialogOptionSelected?.Invoke(requested, __result);
        }
    }  

    public void UpdateSelectedChoice(Vector2 p_direction)
    {
        DialogDisplay __dialogDisplay = _displays[Displays.DIALOG] as DialogDisplay;

        __dialogDisplay.UnselectChoice(_choiceIndex);
        int __direction = Mathf.FloorToInt(p_direction.x + p_direction.y);
        _choiceIndex = HelpExtensions.ClampCircle(_choiceIndex + __direction, 0, _curChoices.Length - 1);
        __dialogDisplay.SelectChoice(_choiceIndex);
    }

    public void FinishDialog()
    {
        requested = null;
        requisitioner = null;
        _dialogIndex = 0;
        _choiceIndex = 0;
    }

    private void ActiveDisplay(Displays p_display, Action p_onShowCompleted, float p_showRatio)
    {
        _activeDisplay = _displays[p_display];
        _activeDisplay.Show(true, p_onShowCompleted, p_showRatio);
    }

    #region Update Display Calls

    public void UpdateDisplay(Displays p_id, int p_operation, bool p_value)
    {
        _displays[p_id].UpdateDisplay(p_operation, p_value);
    }

    public void UpdateDisplay(Displays p_id, int p_operation, float p_value = -99999, float p_data = -99999)
    {
        _displays[p_id].UpdateDisplay(p_operation, p_value, p_data);
    }

    public void UpdateDisplay(Displays p_id, int p_operation, int[] p_data)
    {
        _displays[p_id].UpdateDisplay(p_operation, p_data);
    }

    public void UpdateDisplay(Displays p_id, int p_operation, object p_data)
    {
        _displays[p_id].UpdateDisplay(p_operation, p_data);
    }

    #endregion

    public object GetData(Displays p_id, int p_data)
    {
        return _displays[p_id].GetData(p_data);
    }

    private void OnActionRequested(Displays p_id, int p_action)
    {
        switch (p_id)
        {
            case Displays.INTRO:
                switch (p_action)
                {
                    case 0:
                        break;
                }
                break;
        }
    }

    private void OnDataActionRequested(Displays p_id, int p_action, object p_data)
    {
        switch (p_id)
        {
            case Displays.SHOP:
                switch(p_action)
                {
                    case 0:
                        onItemPurchaseRequested?.Invoke(p_data as ItemData);
                        break;
                    case 1:
                        onItemSellRequested?.Invoke(p_data as ItemData);
                        break;
                    case 2:
                        onShopCloseRequested?.Invoke();
                        break;
                }
                break;
            case Displays.WARDROBE:
                switch (p_action)
                {
                    case 0:
                        onWearClothingRequested?.Invoke(p_data as ClothData);
                        break;
                    case 1:
                        onExitWardrobeRequested?.Invoke();
                        break;
                }
                break;
        }
    }
}