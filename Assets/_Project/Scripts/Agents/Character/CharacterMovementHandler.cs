using System.Collections;
using UnityEngine;

public class CharacterMovementHandler : MonoBehaviour
{
    public float moveSpeed;

    private Vector2 _input;
    private Rigidbody2D _rb;

    public void Initiate()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    public virtual void Tick()
    {

    }

    public virtual void FixedTick()
    {
        Vector2 __velocity = Vector2.zero;

        __velocity += InputVelocity();

        _rb.velocity = __velocity;
    }

    public void SetInput(Vector2 p_input)
    {
        _input = p_input;
    }

    private Vector2 InputVelocity()
    {
        return _input * moveSpeed;
    }


}