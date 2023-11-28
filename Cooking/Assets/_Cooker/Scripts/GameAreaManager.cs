using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum AreaState
{
    StartGame,
    OnSpawn,
    InGame,
    EndGame,
}
public class GameAreaManager : MonoBehaviour
{
    
    AreaState areaState;
    
    //Lv
    int day = 0;

    //Nhân vật
    [SerializeField] private GameObject boxHuman;
    [SerializeField] GameObject chefPrefab;
    [SerializeField] GameObject chefCookerPrefab;
    [SerializeField] GameObject chefPreparePrefab;
    [SerializeField] GameObject dishWasherPrefab;

    Chef chef;
    ChefCooker chefCooker;
    ChefPrepare chefPrepare;
    DishWasher dishWasher;

    public Chef Chef { get { return chef; } set { chef = value; } }

    //Menu
    [SerializeField] List<Menu> MenuRestaurant;
    [SerializeField] List<Menu> MenuRestaurantSpecial;
    private Menu menuByDay;
    private Menu menuSpecialByDay;
    public Menu MenuByDay => menuByDay;
    public Menu MenuSpecialByDay => menuSpecialByDay;

    //Nguyên liệu(default)
    [SerializeField] List<Ingredient> ListIngredient;

    //Đánh giá
    [SerializeField] private EvaluateUI evaluateUI;

    //Bàn chờ


    //Khách hàng
    [SerializeField] private GameObject boxCustomer;
    [SerializeField] private int maxCustomer;
    int customerCount = 0;
    [SerializeField] float timeWaitSpawn = 5;
    private List<Customer> spawnedCustomers = new List<Customer>();
    [SerializeField] private Customer[] customerPrefabs;

    //Nồi nấu
    [SerializeField] private GameObject boxItem;
    [SerializeField] private GameObject cookItemPrefab;
    private CookItem cookItem;

    //Tủ lạnh
    [SerializeField] private GameObject fridgeItemPrefab;
    private FridgeItem fridgeItem;

    //Bồn rửa
    [SerializeField] private GameObject washItemPrefab;
    private WashItem washItem;

    //Ba lô
    private Bag bag;

    //Đĩa bẩn
    [SerializeField] private GameObject dirtyPlateItemPrefab;
    private DirtyPlateItem dirtyPlateItem;

    //Bàn ăn
    [SerializeField] private GameObject dinnerTablePrefab;
    private DinnerTable dinnerTable;
    public DinnerTable DinnerTable => dinnerTable;

    //Cửa ra
    [SerializeField] GameObject doorOut;
    public GameObject DoorOut => doorOut;
    void Start()
    {
        areaState = AreaState.StartGame;
    }
   
    
    void Update()
    {
        if(areaState == AreaState.StartGame)
        {
            if (day == 0) day = 1;
            LoadStartGame(day);
            areaState = AreaState.OnSpawn;
        }
        else if(areaState == AreaState.OnSpawn)
        {
            StartCoroutine(SpawnCustomers());
            areaState = AreaState.InGame;
        }
        else if(areaState == AreaState.InGame) 
        {

        }
        else if(areaState == AreaState.EndGame)
        {

        }
    }
    void LoadStartGame(int day)
    {
        //loadNhanVat
        chef = Instantiate(chefPrefab, boxHuman.transform).GetComponent<Chef>();
        chefCooker = Instantiate(chefCookerPrefab, boxHuman.transform).GetComponent<ChefCooker>();
        chefPrepare = Instantiate(chefPreparePrefab, boxHuman.transform).GetComponent<ChefPrepare>();
        dishWasher = Instantiate(dishWasherPrefab, boxHuman.transform).GetComponent<DishWasher>();

        //loadMenu
        menuByDay = MenuRestaurant[day - 1];
        menuSpecialByDay = MenuRestaurantSpecial[day - 1];

        //load nồi nấu
        cookItem = Instantiate(cookItemPrefab, boxItem.transform).GetComponent<CookItem>();

        //load tủ lạnh
        fridgeItem = Instantiate(fridgeItemPrefab, boxItem.transform).GetComponent<FridgeItem>();

        //load bồn rửa
        washItem = Instantiate(washItemPrefab, boxItem.transform).GetComponent<WashItem>();

        //load đĩa bẩn
        dirtyPlateItem = Instantiate(dirtyPlateItemPrefab, boxItem.transform).GetComponent<DirtyPlateItem>();

        //load dinner Table
        dinnerTable = Instantiate(dinnerTablePrefab, boxItem.transform).GetComponent<DinnerTable>();
    }
    private IEnumerator SpawnCustomers()
    {
        while (true)
        {
            if (customerCount < 5)
            {
                Customer randomCustomerPrefab = GetRandomCustomerPrefab();
                GameObject customerObject = Instantiate(randomCustomerPrefab.gameObject, boxCustomer.transform);

                // Lấy component Customer từ GameObject
                Customer customerComponent = customerObject.GetComponent<Customer>();
                customerComponent.gameAreaManager = this;

                // Thêm khách hàng vào danh sách
                spawnedCustomers.Add(customerComponent);

                customerCount++;
            }
            else
            {
                yield return null; // Cho đến khi được gọi lại để tiếp tục spawn
            }

            yield return new WaitForSeconds(timeWaitSpawn);
        }
    }

    private Customer GetRandomCustomerPrefab()
    {
        int randomIndex = UnityEngine.Random.Range(0, customerPrefabs.Length);
        return customerPrefabs[randomIndex];
    }

    // Phương thức để xoá khách hàng khỏi danh sách khi rời đi
    public void RemoveCustomer(Customer customer)
    {
        if(customerCount>=1)
        {
            customerCount--;
        }
        spawnedCustomers.Remove(customer);
        DinnerTable.OutTable(customer);
    }

}

//class GameAreaManagerActor : ReceiveActor
//{
//    private readonly int _maxWaitingTime = 3;
//    private int dayNow = 0;
//    private bool _isGameStarted;
//    private IActorRef _chefCooker = Context.ActorOf(ChefCookerActor.Props(), "ChefCookerActor");
//    private IActorRef _dishWasher = Context.ActorOf(DishWasherActor.Props(), "DishWasherActor");
//    private IActorRef _chefPrepare = Context.ActorOf(ChefPrepareActor.Props(), "ChefPrepareActor");
//    private IActorRef _customer = Context.ActorOf(CustomerActor.Props(), "CustomerActor");

//    public static Props Props()
//    {
//        return Akka.Actor.Props.Create(() => new GameAreaManagerActor());
//    }

//    public GameAreaManagerActor()
//    {
//        Receive<GameAction>(message =>
//        {
//            switch (message)
//            {
//                case InitializeGame action:
                    
//                    if (!_isGameStarted)
//                    {
//                        _isGameStarted = true;
//                        action.GameManager.OnInitializeGame(action.InitData);
//                    }
//                    else
//                    {
//                        var failData = new InitializeGameData(0, 0, 0, 0, 0, false);
//                        action.GameManager.OnInitializeGame(failData);
//                    }
//                    break;

//                case OpenRestaurant openRes:
//                    var todayMenu = openRes.MenuByDay;
//                    var selectedFoodFromMenu = new List<Task> { _customer.Ask(new SelectSomeFoods(todayMenu, dayNow), TimeSpan.FromSeconds(_maxWaitingTime)) };
//                    Task.WhenAll(selectedFoodFromMenu).PipeTo(_chefCooker, Self);
//                    break;
                
//                  case PrepareIngredients prepareIngredients:
//                    //TODO: 
//                      break;
                
//                case SwitchToAutoMode _:
//                    break;

//                case SwitchToManualMode _:
//                    break;
//            }
//        });
//    }
    
//}
