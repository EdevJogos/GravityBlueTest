using System.Collections;
using UnityEngine;

public class NPCMovementHandler : CharacterMovementHandler
{
    public bool doesMove = true;

    public override void FixedTick()
    {
        if (!doesMove)
            return;

        base.FixedTick();
    }
}