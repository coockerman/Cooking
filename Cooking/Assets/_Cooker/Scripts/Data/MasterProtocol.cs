using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class GameScriptableObject : ScriptableObject { }

public abstract record GameData;

public abstract record IngredientStatus : GameData;


/// <summary>
/// gameAction
/// </summary>

[Serializable]
public class ViTriIngredient
{
    public int vitri;
    public Ingredient ingredient;
}
