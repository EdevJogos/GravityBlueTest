using UnityEngine;

public class CharacterAnimationHandler : MonoBehaviour
{
    public SpriteRenderer bodyRenderer;
    public SpriteRenderer outfitRenderer;

    public BodyData bodyData;
    public ClothData clothData;

    //public float animationSpeed;

    public int lastDirIndex = 3;
    private int _animationIndex = 0;
    private float _animationTimer;

    public void Initiate()
    {
        _animationTimer = 0;
    }

    public void Tick(Vector2 p_moveDir)
    {
        //Sets the direction in wich the character is currently moving.
        int __dirIndex = p_moveDir.y > 0 ? 1 : p_moveDir.y < 0 ? 3 : p_moveDir.x > 0 ? 0 : p_moveDir.x < 0 ? 2 : lastDirIndex;
        //Holds the last direction of movement so that the character keps facing it on no input is received.
        if(lastDirIndex != __dirIndex)
        {
            _animationTimer = 0;
            lastDirIndex = __dirIndex;
        }

        //Custom animator to have the body and the clothing in sync and also to be easy to change them.
        _animationTimer -= Time.deltaTime;

        if(_animationTimer <= 0)
        {
            _animationIndex = HelpExtensions.ClampCircle(_animationIndex + 1, 0, 5);

            if(p_moveDir.magnitude > 0)
            {
                bodyRenderer.sprite = bodyData.animationData.walk.GetAnimationData(__dirIndex)[_animationIndex];
                outfitRenderer.sprite = clothData.animationData.walk.GetAnimationData(__dirIndex)[_animationIndex];

                _animationTimer = bodyData.animationData.walk.animationSpeed;
            }
            else
            {
                bodyRenderer.sprite = bodyData.animationData.idle.GetAnimationData(__dirIndex)[_animationIndex];
                outfitRenderer.sprite = clothData.animationData.idle.GetAnimationData(__dirIndex)[_animationIndex];

                _animationTimer = bodyData.animationData.idle.animationSpeed;
            }
        }
    }
}
