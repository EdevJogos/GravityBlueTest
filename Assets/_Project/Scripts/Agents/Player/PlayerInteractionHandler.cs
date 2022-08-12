using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractionHandler : CharacterInteractionHandler
{
    public LayerMask interactionMask;

    private List<IInteract> _interactions = new List<IInteract>();
    private List<IInteract> _priorityInteractions = new List<IInteract>();

    public override void RequestInteraction()
    {
        Collider2D[] __colliders = Physics2D.OverlapCircleAll(transform.position, 2f, interactionMask);

        for (int __i = 0; __i < __colliders.Length; __i++)
        {
            IInteract __IInteract = __colliders[__i].GetComponent<IInteract>();

            if (__IInteract != null)
            {
                _interactions.Add(__IInteract);
            }
        }
        
        if (_interactions.Count > 0)
        {
            Interact(GetHighestPriorityInteraction());
        }
    }

    /// <summary>
    /// Executes the method assigned to the interacter in the interacted object or character.
    /// </summary>
    private void Interact(IInteract p_interact)
    {
        Interacter __interacter = p_interact.Interact();

        switch (p_interact.GetInteraction())
        {
            case Interactions.EXECUTE_ACTION:
                __interacter.ExecuteAction(GetComponent<Character>());
                break;
            case Interactions.PICK_ITEM:
                Debug.Log("Interactions.PICK_ITEM");
                break;
        }

        _interactions.Clear();
        _priorityInteractions.Clear();
    }

    /// <summary>
    /// If the interaction gets more than one interactable, this will return the one with the highest priority of interaction
    /// </summary>
    private IInteract GetHighestPriorityInteraction()
    {
        _priorityInteractions.Clear();

        for (int __i = 0; __i < GameConfig.Game.InteractionsPriority.Count; __i++)
        {
            for (int __j = 0; __j < _interactions.Count; __j++)
            {
                if (_interactions[__j].GetInteraction() == GameConfig.Game.InteractionsPriority[__i])
                {
                    _priorityInteractions.Add(_interactions[__j]);
                }
            }
        }

        return _priorityInteractions[0];
    }
}