using System;
using UnityEngine;

[System.Serializable]
public class Interacter
{
    public Interactions interaction;

    public Func<Item> PickItem { get; set; }
    public Action<Character> ExecuteAction { get; set; }
}
