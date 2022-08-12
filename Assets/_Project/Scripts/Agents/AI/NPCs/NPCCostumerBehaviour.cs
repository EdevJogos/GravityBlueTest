using System.Collections;
using UnityEngine;

public class NPCCostumerBehaviour : NPCBehaviour
{
    public event System.Action onBreadPaid;
    public event System.Action onCostumerExited;

    public Transform[] toBakeryPoints;
    public Transform toCashierPoint;
    public Transform[] exitPoints;
    public BreadTable[] tables;

    private int _state = 0;
    private int _pointIndex = 0;
    private Vector2 _target;
    private BreadTable _selectedTable;

    public override  void Initiate()
    {
        _pointIndex = 0;
        _target = toBakeryPoints[_pointIndex].position;
    }

    public override void Tick()
    {
        input = (_target - (Vector2)transform.position).normalized;

        float __x = Mathf.Abs(input.x);
        float __y = Mathf.Abs(input.y);

        input.Set(__x < 0.5f ? 0 : input.x, __y < 0.5f ? 0 : input.y);

        if(Vector2.Distance(transform.position, _target) <= 0.1f)
        {
            if(_state == 0)
            {
                if(_pointIndex < toBakeryPoints.Length - 1)
                {
                    _pointIndex++;
                    _target = toBakeryPoints[_pointIndex].position;
                }
                else
                {
                    SelectTable();
                    _state = 1;
                }
            }
            else if (_state == 1)
            {
                AudioManager.PlaySFX(SFXOccurrence.PICK_UP_BREAD);
                _selectedTable.RemoveBread();
                _pointIndex = 0;
                _target = toCashierPoint.position;
                _state = 2;
            }
            else if(_state == 2)
            {
                onBreadPaid?.Invoke();

                _target = exitPoints[_pointIndex].position;
                _state = 3;
            }
            else if (_state == 3)
            {
                if(_pointIndex < exitPoints.Length - 1)
                {
                    _pointIndex++;
                    _target = exitPoints[_pointIndex].position;
                }
                else
                {
                    _state = 0;
                    _pointIndex = 0;
                    _target = toBakeryPoints[_pointIndex].position;

                    GetComponent<NPC>().Active(false);

                    onCostumerExited?.Invoke();
                }
            }
        }
    }

    private void SelectTable()
    {
        for (int __i = 0; __i < tables.Length; __i++)
        {
            if(tables[__i].quantity > 0)
            {
                _selectedTable = tables[__i];
                _target = _selectedTable.transform.position;
                break;
            }
        }
    }
}