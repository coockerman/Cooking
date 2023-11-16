using System;
using System.Collections.Generic;
using UnityEngine;

public class Object { }

[Serializable]
public class Ingredient : Object
{
    [SerializeField] private string name;
    public string Name => name;

    [SerializeField] private IngredientStatus status;
    public IngredientStatus Status => status;

    public Ingredient(string name, IngredientStatus status)
    {
        this.name = name;
        this.status = status;
    }
}

public abstract class IngredientStatus : Object { }

public class RawIngredient : IngredientStatus { }

public class ReadyIngredient : IngredientStatus { }

[Serializable]
public class Food : Object
{
    [SerializeField] private string name;
    public string Name => name;

    [SerializeField] private int cookingTime;
    public int CookingTime => cookingTime;

    [SerializeField] private int eatingTime;
    public int EatingTime => eatingTime;

    [SerializeField] private List<Ingredient> ingredients;
    public List<Ingredient> Ingredients => ingredients;

    public Food(string name, int cookingTime, int eatingTime, List<Ingredient> ingredients)
    {
        this.name = name;
        this.cookingTime = cookingTime;
        this.eatingTime = eatingTime;
        this.ingredients = ingredients;
    }
}

[Serializable]
public class Menu : Object
{
    [SerializeField] List<DataFood> dataFoods;
    private List<Food> foods;
    public List<Food> Foods => foods;

    public Menu()
    {
        if (dataFoods == null) return;
        foreach (var data in dataFoods) {
            foods.Add(data.food);
        }
        foods.Reverse();
    }
}

[Serializable]
public abstract class GameAction : Object
{
    [SerializeField] private Action<object> callBack;
    public Action<object> CallBack => callBack;

    protected GameAction(Action<object> callBack)
    {
        this.callBack = callBack;
    }
}

public abstract class GameData : Object { }

public abstract class ActorAction : Object { }

[Serializable]
public class InitializeGameData : GameData
{
    [SerializeField] private int nCustomer;
    public int NCustomer => nCustomer;

    [SerializeField] private int nChef;
    public int NChef => nChef;

    [SerializeField] private int nChefSupporter;
    public int NChefSupporter => nChefSupporter;

    [SerializeField] private int nDishWasher;
    public int NDishWasher => nDishWasher;

    [SerializeField] private int nWaiters;
    public int NWaiters => nWaiters;

    [SerializeField] private bool isSuccess;
    public bool IsSuccess => isSuccess;

    public InitializeGameData(int nCustomer, int nChef, int nChefSupporter, int nDishWasher, int nWaiters, bool isSuccess)
    {
        this.nCustomer = nCustomer;
        this.nChef = nChef;
        this.nChefSupporter = nChefSupporter;
        this.nDishWasher = nDishWasher;
        this.nWaiters = nWaiters;
        this.isSuccess = isSuccess;
    }
}

[Serializable]
public class InitializeGame : GameAction
{
    [SerializeField] private InitializeGameData initData;
    public InitializeGameData InitData => initData;

    public InitializeGame(InitializeGameData initData, Action<object> callBack) : base(callBack)
    {
        this.initData = initData;
    }
}

[Serializable]
public class StartGame : GameAction
{
    [SerializeField] private Dictionary<int, Menu> menuByDay;
    public Dictionary<int, Menu> MenuByDay => menuByDay;

    [SerializeField] private Chef chef;
    public Chef Chef => chef;

    [SerializeField] private ChefCooker chefCooker;
    public ChefCooker ChefCooker => chefCooker;

    public StartGame(Dictionary<int, Menu> menuByDay, Chef chef, ChefCooker chefSupport, Action<object> callBack) : base(callBack)
    {
        this.menuByDay = menuByDay;
        this.chef = chef;
        this.chefCooker = chefSupport;
    }
}

[Serializable]
public class SwitchToAutoMode : GameAction
{
    public SwitchToAutoMode(Action<object> callBack) : base(callBack) { }
}

[Serializable]
public class SwitchToManualMode : GameAction
{
    public SwitchToManualMode(Action<object> callBack) : base(callBack) { }
}

[Serializable]
public class StartJob : ActorAction
{
    [SerializeField] private object data;
    public object Data => data;

    [SerializeField] private object monoBehaviour;
    public object MonoBehaviour => monoBehaviour;

    public StartJob(object data, object monoBehavior)
    {
        this.data = data;
        this.monoBehaviour = monoBehavior;
    }
}

[Serializable]
public class ProcessMaterial : GameAction
{
    [SerializeField] private object data;
    public object Data => data;

    public ProcessMaterial(object data, Action<object> callBack) : base(callBack)
    {
        this.data = data;
    }
}

[Serializable]
public class ReveiceFood : ActorAction
{
    [SerializeField] private object data;
    public object Data => data;

    [SerializeField] private object monoBehaviour;
    public object MonoBehaviour => monoBehaviour;
    public ReveiceFood(object data, Action<object> monoBehaviour)
    {
        this.data = data;
        this.monoBehaviour = monoBehaviour;
    }
}
public class Order : ActorAction
{
    [SerializeField] private object data;
    public object Data => data;

    [SerializeField] private object monoBehaviour;
    public object MonoBehaviour => monoBehaviour;

    public Order(object data, object monoBehavior)
    {
        this.data = data;
        this.monoBehaviour = monoBehavior;
    }
}
//public class Chef : Object { }  // You might want to define the Chef class

//public class ChefSupport : Object { }  // You might want to define the ChefSupport class

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/DataFood", order = 1)]
public class DataFood : ScriptableObject
{
    public Food food;
}
