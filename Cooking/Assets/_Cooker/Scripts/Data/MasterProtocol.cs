using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class GameScriptableObject : ScriptableObject { }


public abstract class GameData { }

public class InitializeGameData : GameData
{
    public int NCustomer { get; }
    public int NChef { get; }
    public int NChefSupporter { get; }
    public int NDishWasher { get; }
    public int NWaiters { get; }
    public bool IsSuccess { get; }
    public InitializeGameData(int nCustomer, int nChef, int nChefSupporter, int nDishWasher, int nWaiters, bool isSuccess)
    {
        NCustomer = nCustomer;
        NChef = nChef;
        NChefSupporter = nChefSupporter;
        NDishWasher = nDishWasher;
        NCustomer = nWaiters;
        IsSuccess = isSuccess;
    }
}


public abstract class IngredientStatus : GameData { }

public class RawIngredient : IngredientStatus { }

public class ReadyIngredient : IngredientStatus { }


/// <summary>
///  GAME ACTIONS
///  Using for communicate between monoBehaviors and actors
/// </summary>
public abstract class GameAction { }

public class InitializeGame : GameAction
{
    public InitializeGameData InitData { get; }
    public GameAreaManager GameManager { get; }

    public InitializeGame(InitializeGameData initData, GameAreaManager gameManager) : base()
    {
        InitData = initData;
        GameManager = gameManager;
    }
}

public class SelectSomeFoods : GameAction
{
    public List<Menu> MenuToSelected;

    public SelectSomeFoods(List<Menu> menuToSelected)
    {
        MenuToSelected = menuToSelected;
    }
}

public class OrderFood : GameAction
{
    public List<Menu> OrderList;

    public OrderFood(List<Menu> orderList)
    {
        OrderList = orderList;
    }
}

public class PrepareIngredients : GameAction
{
    public List<Ingredient> RequireIngredients { get; }

    public PrepareIngredients(List<Ingredient> requireIngredients)
    {
        RequireIngredients = requireIngredients;
    }
}

public class OpenRestaurant : GameAction
{
    public List<Menu> MenuByDay { get; }
    public Customer Customer { get; }

    public OpenRestaurant(List<Menu> menuByDay, Customer customer)
    {
        MenuByDay = menuByDay;
        Customer = customer;
    }
}
public class StartCooking : GameAction
{
   
}



public class SwitchToAutoMode : GameAction
{
    public SwitchToAutoMode() : base() { }
}

public class SwitchToManualMode : GameAction
{
    public SwitchToManualMode() : base() { }
}


