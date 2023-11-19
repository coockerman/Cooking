using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Akka.Actor;
using UnityEngine;

public class GameAreaManager : MonoBehaviour
{

    [SerializeField] private int customerEachRound;
    [SerializeField] private int totalChefs;
    [SerializeField] private int totalChefSupporter;
    [SerializeField] private int totalDishWasher;
    [SerializeField] private int totalWaiter;
    
    [SerializeField] private ChefCooker cookerPrefab;
    [SerializeField] private ChefPrepare chefPreparePrefab;
    [SerializeField] private Customer customerPrefabs;
    
    [SerializeField] List<Menu> MenuRestaurant;    
    
    private readonly ActorSystem _gameActorSystem = ActorSystem.Create("CookGameActorSystem");
    private IActorRef _gameManager;

    void Start()
    {
        _gameManager = _gameActorSystem.ActorOf(GameAreaManagerActor.Props(), "GameManagerActor");
        var initGameData = new InitializeGameData(customerEachRound, totalChefs, totalChefSupporter, totalDishWasher, totalWaiter, true);
        _gameManager.Tell(new InitializeGame(initGameData, this));
    }
   
    
    void Update()
    {
        
    }

    public void OnInitializeGame(object o)
    {
        var initGameData = (InitializeGameData)o;
        if (!initGameData.IsSuccess)
        {
            Debug.Log("Init game fail");
            return;
        }
        
        //TODO: 1. Spawn prefab for each number of objectives below
        var maxCustomers = initGameData.NCustomer;
        var totalChefs = initGameData.NChef;
        var totalChefSupports = initGameData.NChefSupporter;
        var totalWaiters = initGameData.NWaiters;
        var totalDishWashers = initGameData.NDishWasher;
        
        var allIngredients = LoadAllIngredients();
        
        _gameManager.Tell(new PrepareIngredients(allIngredients));
        _gameManager.Tell(new StartCooking());//TODO
        _gameManager.Tell(new OpenRestaurant(MenuRestaurant,customerPrefabs));
    }
    
    private List<Ingredient> LoadAllIngredients()
    {
        //TODO: 2. Implement function body, load all ingredients from scriptable objects
        return new List<Ingredient> { new Ingredient() };
    }

    private void OnStartGame(object o)
    {
        Debug.Log("Game started");
    }

}

class GameAreaManagerActor : ReceiveActor
{
    private readonly int _maxWaitingTime = 3;
    private bool _isGameStarted;
    private IActorRef _chefCooker = Context.ActorOf(ChefCookerActor.Props(), "ChefCookerActor");
    private IActorRef _dishWasher = Context.ActorOf(DishWasherActor.Props(), "DishWasherActor");
    private IActorRef _chefPrepare = Context.ActorOf(ChefPrepareActor.Props(), "ChefPrepareActor");
    private IActorRef _customer = Context.ActorOf(CustomerActor.Props(), "CustomerActor");

    public static Props Props()
    {
        return Akka.Actor.Props.Create(() => new GameAreaManagerActor());
    }

    private GameAreaManagerActor()
    {
        Receive<GameAction>(message =>
        {
            switch (message)
            {
                case InitializeGame action:
                    
                    if (!_isGameStarted)
                    {
                        _isGameStarted = true;
                        action.GameManager.OnInitializeGame(action.InitData);
                    }
                    else
                    {
                        var failData = new InitializeGameData(0, 0, 0, 0, 0, false);
                        action.GameManager.OnInitializeGame(failData);
                    }
                    break;

                case OpenRestaurant openRes:
                    var todayMenu = openRes.MenuByDay;
                    var selectedFoodFromMenu = new List<Task> { _customer.Ask(new SelectSomeFoods(todayMenu), TimeSpan.FromSeconds(_maxWaitingTime)) };
                    Task.WhenAll(selectedFoodFromMenu).PipeTo(_chefCooker, Self);
                    break;
                
                  case PrepareIngredients prepareIngredients:
                    //TODO: 
                      break;
                
                case SwitchToAutoMode _:
                    break;

                case SwitchToManualMode _:
                    break;
            }
        });
    }
    
}
