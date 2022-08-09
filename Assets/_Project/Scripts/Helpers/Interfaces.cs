using UnityEngine;

public interface IDestructable
{
    void RequestDestroy();
}

public interface IDatabase
{
    void Initiate();
}

public interface IInteract
{
    bool CanInteract { get; }
    Interactions GetInteraction();
    Interacter Interact();
}

public interface IInputHandler
{
    void Enable(bool p_enable);
}