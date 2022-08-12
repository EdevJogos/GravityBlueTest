using System.Collections;
using UnityEngine;

public class NPCBehaviour : MonoBehaviour
{
    public Vector2 input;

    public virtual void Initiate() { }
    public virtual void Tick() { }
}