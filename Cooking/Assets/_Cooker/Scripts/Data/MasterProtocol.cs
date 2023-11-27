using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class GameScriptableObject : ScriptableObject { }


/// <summary>
/// gameData
/// </summary>
public abstract record GameData;

public record InitializeGameData(int NCustomer, int NChef, int NChefCooker, int NDishWasher, int NChefPrepare, bool IsSuccess) : GameData;

public abstract record IngredientStatus : GameData;

public record RawIngredient : IngredientStatus;

public record ReadyIngredient : IngredientStatus;


/// <summary>
/// gameAction
/// </summary>
public abstract record GameAction;

public record InitializeGame(InitializeGameData InitData, GameAreaManager GameManager) : GameAction;

public record SelectSomeFoods(List<Menu> MenuToSelected, int dayNow) : GameAction;

public record OrderFood(List<Menu> OrderList) : GameAction;

public record PrepareIngredients(List<Ingredient> RequireIngredients) : GameAction;

public record OpenRestaurant(List<Menu> MenuByDay, Customer Customer) : GameAction;

public record StartCooking : GameAction;

public record SwitchToAutoMode : GameAction;

public record SwitchToManualMode : GameAction;
public record StartWaiting(Food foodOrder) :GameAction;


