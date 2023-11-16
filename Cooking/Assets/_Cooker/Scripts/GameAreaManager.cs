using System;
using System.Collections;
using System.Collections.Generic;
using Akka.Actor;
using UnityEngine;

public class GameAreaManager : MonoBehaviour
{
    public static GameAreaManager Instance;

    [SerializeField] private int customerEachRound;
    [SerializeField] private int totalChefs;
    [SerializeField] private int totalChefSupporter;
    [SerializeField] private int totalDishWasher;
    [SerializeField] private int totalWaiter;
    
    [SerializeField] private Chef chefPrefab;
    [SerializeField] private ChefCooker chefSupportPrefab;
    
    //private Dictionary<int, Menu> MenuByDay;
    
    [SerializeField] Menu FoodDay1;    
    [SerializeField] Menu FoodDay2;
    [SerializeField] Menu FoodDay3;

    [SerializeField] List<GameObject> typePrefabCustomer;

    [SerializeField] List<GameObject> Tables;
    [SerializeField] GameObject door;
    [SerializeField] float delayTime;

    private readonly ActorSystem _gameActorSystem = ActorSystem.Create("CookGameActorSystem");
    private IActorRef _gameManager;

    bool isSpawn = true;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogWarning("Duplicate instance of GameAreaManager. Destroying this instance.");
            Destroy(this.gameObject);
        }
    }
    void Start()
    {
        StartCoroutine(SpawnCustomersPeriodically());

        _gameManager = _gameActorSystem.ActorOf(GameAreaManagerActor.Props(), "GameManagerActor");
        var _chefCooker = _gameActorSystem.ActorOf(ChefCookerActor.Props(), "ChefCookerActor");
        var initGameData = new InitializeGameData(customerEachRound, totalChefs, totalChefSupporter, totalDishWasher, totalWaiter, true);
        _gameManager.Tell(new InitializeGame(initGameData, OnInitializeGame));
        _gameManager.Tell(new Order(initGameData, new GameObject()));
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
        var menus = new Dictionary<int, Menu>()
        {
            {1, FoodDay1 },
            {2, FoodDay2 },
            {3, FoodDay3 }
        };
        return menus;

    }
    
    //private Dictionary<int, Menu> LoadMenuByDay()
    //{
    //    //TODO: Help me load menu by day from database or another technicals
    //    var food1 = new Food("FoodName1", 2, 2, new List<Ingredient> { new Ingredient("RawIngredient1", new RawIngredient()) });
    //    var food2 = new Food("FoodName2", 2, 2, new List<Ingredient> { new Ingredient("RawIngredient2", new RawIngredient()) });
    //    var food3 = new Food("FoodName3", 2, 2, new List<Ingredient> { new Ingredient("RawIngredient3", new RawIngredient()) });
    //    var food4 = new Food("FoodName4", 2, 2, new List<Ingredient> { new Ingredient("RawIngredient4", new RawIngredient()) });
    //    var food5 = new Food("FoodName5", 2, 2, new List<Ingredient> { new Ingredient("RawIngredient5", new RawIngredient()) });
    //    var food6 = new Food("FoodName6", 2, 2, new List<Ingredient> { new Ingredient("RawIngredient6", new RawIngredient()) });
    //    var food7 = new Food("FoodName7", 2, 2, new List<Ingredient> { new Ingredient("RawIngredient7", new RawIngredient()) });
    //    var menus = new Dictionary<int, Menu>()
    //    {
    //        { 1, new Menu(new List<Food> { food1, food2, food3 }) },
    //        { 2, new Menu(new List<Food> { food4, food5, food3 }) },
    //        { 3, new Menu(new List<Food> { food6, food2, food7 }) }
    //    };
    //    return menus;
    //}

    private void OnStartGame(object o)
    {
        Debug.Log("Game started");
    }
    IEnumerator SpawnCustomersPeriodically()
    {
        while (true)
        {
            if(isSpawn)
            {
                SpawnCustomer();
            }
            yield return new WaitForSeconds(delayTime);
        }
    }
    void SpawnCustomer()
    {
        if (typePrefabCustomer.Count > 0)
        {
            int randomIndex = UnityEngine.Random.Range(0, typePrefabCustomer.Count);
            GameObject randomPrefab = typePrefabCustomer[randomIndex];
            Instantiate(randomPrefab, door.transform.position, Quaternion.identity);
        }
        else
        {
            Debug.LogError("Không có đối tượng nào trong danh sách typePrefabCustomer.");
        }
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
                case Order foodOrder:
                    HandleCustomerOrder(foodOrder);
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
        _chefCooker.Tell(new StartJob(startGameAction.MenuByDay, startGameAction.ChefCooker));
    }

    private void HandleAutoMode()
    {

    }

    private void HandleManualMode()
    {

    }
    void HandleCustomerOrder(Order foodOrder)
    {
        _chefCooker.Tell(new Order(foodOrder.Data, foodOrder.MonoBehaviour));
        _dishWasher.Tell(new Order(foodOrder.Data, foodOrder.MonoBehaviour));
        _chefPrepare.Tell(new Order(foodOrder.Data, foodOrder.MonoBehaviour));
    }
}
