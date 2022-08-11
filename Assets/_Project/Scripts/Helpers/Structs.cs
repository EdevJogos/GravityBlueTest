using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public struct Range
{
    /// <summary>
    /// Returns a random value between min and max.
    /// </summary>
    public float Value { get { return Random.Range(min, max); } }

    public float min;
    public float max;

    public Range(float min, float max)
    {
        this.min = min;
        this.max = max;
    }
}

public class RandomNoRepeat
{
    private IEnumerable<int> _range;
    private List<int> _indexes = new List<int>();

    /// <summary>
    /// Do not construct this object outside of a method scope.
    /// </summary>
    public RandomNoRepeat(IEnumerable<int> p_range)
    {
        _range = p_range;
        _indexes.Clear();
        _indexes.AddRange(_range);
    }

    /// <summary>
    /// Returns a random number without reapeting previous returned ones.
    /// </summary>
    public int GetRandom()
    {
        int __radomIndex = Random.Range(0, _indexes.Count - 1);
        int __chosenNumber = _indexes[__radomIndex];

        _indexes.RemoveAt(__radomIndex);

        if(_indexes.Count == 0)
        {
            _indexes.AddRange(_range);
        }

        return __chosenNumber;
    }

    /// <summary>
    /// Resets the previous choosen values, allowing them to be selected again.
    /// </summary>
    public void Restart()
    {
        _indexes.Clear();
        _indexes.AddRange(_range);
    }
}

[System.Serializable]
public struct ShopData
{
    public ItemsTypes itemType;
    public Sprite panelSprite;
    public Sprite itemBarSprite;
    public Sprite buttonNormalSprite;
    public Sprite buttonPressedSprite;
}

[System.Serializable]
public struct Dialog
{
    [NonReorderable] public int[] condition;
    [NonReorderable] public DialogChoice[] choices;
    [NonReorderable] public DialogLine[] dialogLines;
}


/// <summary>
/// Can be used to add portrait and other differente information for each text line.
/// </summary>
[System.Serializable]
public struct DialogLine
{
    public string textLine;
}

[System.Serializable]
public struct DialogChoice
{
    public DialogActions id;
    public int data;
    public string choiceText;
}

public struct DialogResult
{
    public DialogActions action;
    public int data;

    public DialogResult(DialogActions p_action, int p_data)
    {
        action = p_action;
        data = p_data;
    }
}

[System.Serializable]
public struct SpriteAnimationData
{
    public FourDirectionAnimationData idle, walk;
}

[System.Serializable]
public struct FourDirectionAnimationData
{
    public float animationSpeed;
    public Sprite[] front, back, left, right;

    public Sprite[] GetAnimationData(int p_direction)
    {
        switch (p_direction)
        {
            case 0:
                return left;
            case 1:
                return back;
            case 2:
                return right;
            case 3:
                return front;
        }

        return null;
    }
}