using UnityEngine;

public class BreadTable : MonoBehaviour
{
    public int quantity;
    public Sprite emptySprite, filledSprite;
    public SpriteRenderer spriteRenderer;

    public void AddBread()
    {
        quantity++;

        Filled(true);
    }

    public void RemoveBread()
    {
        quantity = HelpExtensions.ClampMin0(quantity - 1);

        if (quantity == 0) Filled(false);
    }

    private void Filled(bool p_filled)
    {
        spriteRenderer.sprite = p_filled ? filledSprite : emptySprite;
    }
}
