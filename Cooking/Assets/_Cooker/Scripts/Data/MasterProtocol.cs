using System;
using System.Collections.Generic;
using UnityEngine;

//----------------------------------------------------------------------------------------------------------------------------//
//----------------------------------------------------------------------------------------------------------------------------//
public class GameScriptableObject : ScriptableObject { }


//----------------------------------------------------------------------------------------------------------------------------//
//----------------------------------------------------------------------------------------------------------------------------//
public abstract class IngredientStatus { }

public class RawIngredient : IngredientStatus { }

public class ReadyIngredient : IngredientStatus { }




//----------------------------------------------------------------------------------------------------------------------------//
//----------------------------------------------------------------------------------------------------------------------------//
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




//----------------------------------------------------------------------------------------------------------------------------//
//----------------------------------------------------------------------------------------------------------------------------//
public abstract class GameAction
{
    public Action<object> CallBack { get ;}
    protected GameAction(Action<object> callBack)
    {
        CallBack = callBack;
    }
}
public class InitializeGame : GameAction
{
    public InitializeGameData InitData { get; }
    public InitializeGame(InitializeGameData initData, Action<object> callBack) : base(callBack)
    {
        InitData = initData;
    }
}
public class StartGame : GameAction
{
    public List<Menu> MenuByDay { get; }
    public Chef Chef { get; }
    public ChefCooker ChefCooker { get; }

    public StartGame(List<Menu> menuByDay, Chef chef, ChefCooker chefSupport, Action<object> callBack) : base(callBack)
    {
        MenuByDay = menuByDay;
        Chef = chef;
        ChefCooker = chefSupport;
    }
}
public class SwitchToAutoMode : GameAction
{
    public SwitchToAutoMode(Action<object> callBack) : base(callBack) { }
}

public class SwitchToManualMode : GameAction
{
    public SwitchToManualMode(Action<object> callBack) : base(callBack) { }
}
public class ProcessMaterial : GameAction
{
    public object Data { get; }

    public ProcessMaterial(object data, Action<object> callBack) : base(callBack)
    {
        Data = data;
    }
}





//----------------------------------------------------------------------------------------------------------------------------//
//----------------------------------------------------------------------------------------------------------------------------//
public abstract class ActorAction { }
public class StartJob : ActorAction
{
    public object Data { get; }
    public object MonoBehaviour { get; }

    public StartJob(object data, object monoBehavior)
    {
        Data = data;
        MonoBehaviour = monoBehavior;
    }
}
public class ReveiceFood : ActorAction
{
    public object Data { get; }
    public object MonoBehaviour { get; }

    public ReveiceFood(object data, Action<object> monoBehaviour)
    {
        Data = data;
        MonoBehaviour = monoBehaviour;
    }
}
public class Order : ActorAction
{
    public object Data { get; }
    public object MonoBehaviour { get; }

    public Order(object data, object monoBehavior)
    {
        Data = data;
        MonoBehaviour = monoBehavior;
    }
}



