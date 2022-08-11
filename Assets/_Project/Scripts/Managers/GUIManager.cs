using UnityEngine;
using System;
using System.Collections.Generic;

public class GUIManager : MonoBehaviour
{
    public event Action<ItemData> onItemPurchaseRequested;
    public event Action<ItemData> onItemSellRequested;
    public event Action onShopCloseRequested;
    public event Action<ClothData> onWearClothingRequested;
    public event Action onExitWardrobeRequested;

    public Transform displaysHolder;

    private int _dialogIndex = 0;
    private int _choiceIndex = 0;
    private DialogChoice[] _curChoices;

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

    public void ShowDialog(string p_requested, string p_requisitioner, Dialog p_dialog)
    {
        ShowDisplay(Displays.DIALOG);

        DialogDisplay __dialogDisplay = _displays[Displays.DIALOG] as DialogDisplay;

        __dialogDisplay.UpdateDialogBox(p_requested, p_dialog.dialogLines[_dialogIndex].textLine);
        _dialogIndex++;
        
        if (p_dialog.dialogLines.Length == _dialogIndex)
        {
            if(p_dialog.choices.Length > 0)
            {
                _curChoices = p_dialog.choices;
                __dialogDisplay.UpdateChoicesBox(p_requisitioner, p_dialog.choices);
                __dialogDisplay.SelectChoice(0);
            }
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

    public DialogResult GetDialogResult()
    {
        return new DialogResult(_curChoices[_choiceIndex].id, _curChoices[_choiceIndex].data);
    }

    public void FinishDialog()
    {
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