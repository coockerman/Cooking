using System;
using System.Collections.Generic;
using UnityEngine;

public abstract record Object;
public abstract record GameAction(Action<object> CallBack);


//Ingredients
public abstract record IngredientStatus;
public record RawIngredient : IngredientStatus;
public record ReadyIngredient : IngredientStatus;

public record Ingredient(string Name, IngredientStatus Status);


//Object
public record Food(string Name, int CookingTime, int EatingTime, int DayAppear, List<Ingredient> Ingredients) : Object();

public record Menu(List<Food> Foods) : Object();


//GameAction
public record StartGame(InitializeGame InitializeGameData, Action<object> CallBack) : GameAction(CallBack);

public record InitializeGame(int NCustomer, int NChef, int NChefSupporter, int NDishWasher, int NWaiters, Action<object> CallBack) : GameAction(CallBack);

public record SwitchToAutoMode(Action<object> CallBack) : GameAction(CallBack);
public record SwitchToManualMode(Action<object> CallBack) : GameAction(CallBack);

public record InitMenuFromDataBase(Action<object> CallBack) : GameAction(CallBack);



