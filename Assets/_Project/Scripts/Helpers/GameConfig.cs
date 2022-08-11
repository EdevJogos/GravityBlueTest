using System.Collections.Generic;
using UnityEngine;

public static class GameConfig
{
    public struct Game
    {
        public static List<Interactions> InteractionsPriority = new List<Interactions>(3)
        {
            { Interactions.NPC },
            { Interactions.WARDROBE },
            { Interactions.PICK_ITEM },
        };
    }

    public struct Character
    {

    }
}
