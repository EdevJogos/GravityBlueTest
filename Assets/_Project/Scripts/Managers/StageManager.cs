using System.Collections;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public event System.Action onBreadSold;

    public BreadTable[] tables;
    public NPC[] costumers;

    private int _activeCostumers;
    private float _checkTimer = 1f;
    private Range _checkTimeRange;

    private void Start()
    {
        _checkTimeRange = new Range(4f, 8f);

        for (int __i = 0; __i < costumers.Length; __i++)
        {
            costumers[__i].GetComponent<NPCCostumerBehaviour>().onBreadPaid += StageManager_onBreadPaid;
            costumers[__i].GetComponent<NPCCostumerBehaviour>().onCostumerExited += StageManager_onCostumerExited;
        }
    }

    private void Update()
    {
        _checkTimer -= Time.deltaTime;

        if(_checkTimer <= 0)
        {
            if(_activeCostumers < TotalBreads())
            {
                ActiveCostumer();
            }

            _checkTimer = _checkTimeRange.Value;
        }
    }

    private void ActiveCostumer()
    {
        for (int __i = 0; __i < costumers.Length; __i++)
        {
            if(!costumers[__i].IsActive)
            {
                costumers[__i].Active(true);
                _activeCostumers++;
                break;
            }
        }
    }

    private int TotalBreads()
    {
        int __total = 0;

        for (int __i = 0; __i < tables.Length; __i++)
        {
            __total += tables[__i].quantity;
        }

        return __total;
    }

    private void StageManager_onCostumerExited()
    {
        _activeCostumers--;
    }

    private void StageManager_onBreadPaid()
    {
        onBreadSold?.Invoke();
    }
}
