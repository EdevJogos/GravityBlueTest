using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "ClothData", menuName = "ScriptableObjects/Items/ClothData", order = 2)]
[System.Serializable]
public class ClothData : ItemData
{
    public SpriteAnimationData animationData;
}