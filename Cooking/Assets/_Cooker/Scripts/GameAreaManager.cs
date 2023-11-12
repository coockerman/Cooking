using System;
using System.Collections.Generic;
using Akka.Actor;
using UnityEngine;

public class GameAreaManager : MonoBehaviour
{
    [SerializeField] private int customerEachRound;
    [SerializeField] private int totalChefs;
    [SerializeField] private int totalChefSupporter;
    [SerializeField] private int totalDishWasher;
    [SerializeField] private int totalWaiter;
    
    [SerializeField] private Chef chefPrefab;
    [SerializeField] private ChefSupport chefSupportPrefab;
    
    private Dictionary<int, Menu> MenuByDay;
    
    private readonly ActorSystem _gameActorSystem = ActorSystem.Create("CookGameActorSystem");
    private IActorRef _gameManager;

    void Start()
    {
        _gameManager = _gameActorSystem.ActorOf(GameAreaManagerActor.Props(), "GameManagerActor");
        var initGameData = new InitializeGameData(customerEachRound, totalChefs, totalChefSupporter, totalDishWasher, totalWaiter, true);
        _gameManager.Tell(new InitializeGame(initGameData, OnInitializeGame));
    }

    private void OnInitializeGame(object o)
    {
        var initGameData = (InitializeGameData)o;
        if (!initGameData.IsSuccess)
        {
            Debug.Log("Init game fail");
            //TODO: Handle logic when init game fail here
        }
        else
        {
            Debug.Log("Init game success");
            var menuByDay = LoadMenuByDay();
            Debug.Log("Init game success");

            _gameManager.Tell(new StartGame(menuByDay, chefPrefab, chefSupportPrefab, OnStartGame));
            //TODO: Handle logic when init game success here
            Debug.Log("Init game success");
        }
    }

    private Dictionary<int, Menu> LoadMenuByDay()
    {
        //TODO: Help me load menu by day from database or another technicals
        var food1 = new Food("FoodName1", 2, 2, new List<Ingredient> { new Ingredient("RawIngredient1", new RawIngredient()) });
        var food2 = new Food("FoodName2", 2, 2, new List<Ingredient> { new Ingredient("RawIngredient2", new RawIngredient()) });
        var food3 = new Food("FoodName3", 2, 2, new List<Ingredient> { new Ingredient("RawIngredient3", new RawIngredient()) });
        var food4 = new Food("FoodName4", 2, 2, new List<Ingredient> { new Ingredient("RawIngredient4", new RawIngredient()) });
        var food5 = new Food("FoodName5", 2, 2, new List<Ingredient> { new Ingredient("RawIngredient5", new RawIngredient()) });
        var food6 = new Food("FoodName6", 2, 2, new List<Ingredient> { new Ingredient("RawIngredient6", new RawIngredient()) });
        var food7 = new Food("FoodName7", 2, 2, new List<Ingredient> { new Ingredient("RawIngredient7", new RawIngredient()) });
        var menus = new Dictionary<int, Menu>()
        {
            { 1, new Menu(new List<Food> { food1, food2, food3 }) },
            { 2, new Menu(new List<Food> { food4, food5, food3 }) },
            { 3, new Menu(new List<Food> { food6, food2, food7 }) }
        };
        return menus;
    }
    
    private void OnStartGame(object o)
    {
        Debug.Log("Game started");
    }
    
    void Update()
    {
        
    }
}

class GameAreaManagerActor : ReceiveActor
{
    private Boolean _isGameStarted;
    private IActorRef _chef = Context.ActorOf(ChefActor.Props(), "ChefActor");
    private IActorRef _chefSupport = Context.ActorOf(ChefSupportActor.Props(), "ChefSupportActor");
    
    public static Props Props()
    {
        return Akka.Actor.Props.Create(() => new GameAreaManagerActor());
    }
    
    public GameAreaManagerActor()
    {
        Receive<GameAction>(message =>
        {
            switch (message)
            {
                case InitializeGame initGameAction:
                    InitGameData(initGameAction);
                    break;
                case StartGame startGameAction:
                    StartGame(startGameAction);
                    break;
                case SwitchToAutoMode _:
                    HandleAutoMode();
                    break;
                case SwitchToManualMode _:
                    HandleManualMode();
                    break;
            }
        });
    }
    
    private void InitGameData(InitializeGame initGameAction)
    {
        var failData = new InitializeGameData(0,0,0,0,0, false);
        if (!_isGameStarted)
        {
            initGameAction.CallBack(initGameAction.InitData);
            _isGameStarted = true;
        }
        else
        {
            initGameAction.CallBack(failData);
        }
    }

    private void StartGame(StartGame startGameAction)
    {
        Debug.Log("Receive start tell");
        _chefSupport.Tell(new StartJob(startGameAction.MenuByDay, startGameAction.ChefSupport));
    }

    private void HandleAutoMode()
    {

    }

    private void HandleManualMode()
    {

    }

}
