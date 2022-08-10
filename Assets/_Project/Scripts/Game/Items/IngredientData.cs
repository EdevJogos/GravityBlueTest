using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "IngredientData", menuName = "ScriptableObjects/Items/IngredientData", order = 1)]
[System.Serializable]
public class IngredientData : ItemData
{
    public Ingredients ID;
}