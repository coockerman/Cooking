using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class GameScriptableObject : ScriptableObject { }

public abstract record GameData;

public record InitializeGameData(int NCustomer, int NChef, int NChefSupporter, int NDishWasher, int NWaiters, bool IsSuccess) : GameData;

public abstract record IngredientStatus : GameData;

public record RawIngredient : IngredientStatus;

public record ReadyIngredient : IngredientStatus;

public abstract record GameAction;

public record InitializeGame(InitializeGameData InitData, GameAreaManager GameManager) : GameAction;

public record SelectSomeFoods(List<Menu> MenuToSelected) : GameAction;

public record OrderFood(List<Menu> OrderList) : GameAction;

public record PrepareIngredients(List<Ingredient> RequireIngredients) : GameAction;

public record OpenRestaurant(List<Menu> MenuByDay, Customer Customer) : GameAction;

public record StartCooking : GameAction;

public record SwitchToAutoMode : GameAction;

public record SwitchToManualMode : GameAction;


