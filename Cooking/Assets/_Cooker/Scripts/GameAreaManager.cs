using System;
using System.Collections;
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
    [SerializeField] private ChefCooker chefSupportPrefab;

    [SerializeField] List<Menu> MenuRestaurant;    

    [SerializeField] List<GameObject> typePrefabCustomer;


    private readonly ActorSystem _gameActorSystem = ActorSystem.Create("CookGameActorSystem");
    private IActorRef _gameManager;

    void Start()
    {
        _gameManager = _gameActorSystem.ActorOf(GameAreaManagerActor.Props(), "GameManagerActor");
        var _chefCooker = _gameActorSystem.ActorOf(ChefCookerActor.Props(), "ChefCookerActor");
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
            Debug.Log("Init game success");

            _gameManager.Tell(new StartGame(MenuRestaurant, chefPrefab, chefSupportPrefab, OnStartGame));
            //TODO: Handle logic when init game success here
            Debug.Log("Init game success");
        }
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
    private bool _isGameStarted;
    private IActorRef _chef = Context.ActorOf(ChefActor.Props(), "ChefActor");
    private IActorRef _chefCooker = Context.ActorOf(ChefCookerActor.Props(), "ChefCookerActor");
    private IActorRef _dishWasher = Context.ActorOf(DishWasherActor.Props(), "DishWasherActor");
    private IActorRef _chefPrepare = Context.ActorOf(ChefPrepareActor.Props(), "ChefPrepareActor");
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
        Receive<ActorAction>(message =>
        {
            switch (message)
            {
                
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
        _chefCooker.Tell(new StartJob(startGameAction.MenuByDay, startGameAction.ChefCooker));
    }

    private void HandleAutoMode()
    {

    }

    private void HandleManualMode()
    {

    }
    
}
