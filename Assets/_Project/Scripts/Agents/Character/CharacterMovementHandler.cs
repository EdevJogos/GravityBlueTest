using System.Collections;
using UnityEngine;

public class CharacterMovementHandler : MonoBehaviour
{
    public float moveSpeed;
    public AudioSource footSteps;

    [System.NonSerialized] public Vector2 input;

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
        input = p_input;

        if(input.magnitude > 0)
        {
            if(footSteps != null && !footSteps.isPlaying) 
                footSteps.Play();
        }
        else
        {
            if (footSteps != null && footSteps.isPlaying) 
                footSteps.Stop();
        }
    }

    private Vector2 InputVelocity()
    {
        return input * moveSpeed;
    }


}