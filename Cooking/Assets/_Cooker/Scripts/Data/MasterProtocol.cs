using System;
using System.Collections.Generic;

public abstract record Object;
public record Ingredient(string Name, IngredientStatus Status);
public abstract record IngredientStatus;
public record RawIngredient : IngredientStatus;
public record ReadyIngredient : IngredientStatus;

public record Food(string Name, int CookingTime, int EatingTime, List<Ingredient> Ingredients) : Object;
public record Menu(List<Food> Foods) : Object;


public abstract record GameAction(Action<object> CallBack);
public abstract record GameData;

public abstract record ActorAction;

public record InitializeGameData(int NCustomer, int NChef, int NChefSupporter, int NDishWasher, int NWaiters, bool IsSuccess): GameData;
public record InitializeGame(InitializeGameData InitData, Action<object> CallBack) : GameAction(CallBack);
public record StartGame(Dictionary<int, Menu> MenuByDay, Chef Chef, ChefSupport ChefSupport, Action<object> CallBack) : GameAction(CallBack);
public record SwitchToAutoMode(Action<object> CallBack) : GameAction(CallBack);
public record SwitchToManualMode(Action<object> CallBack) : GameAction(CallBack);

public record StartJob(object data, object monoBehavior) : ActorAction;

public record ProcessMaterial(object data, Action<object> CallBack) : GameAction(CallBack);

