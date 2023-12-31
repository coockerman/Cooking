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
    
    //Lv(day 1-3)
    int day = 0;

    //Nhân vật
    [SerializeField] private GameObject boxHuman;
    [SerializeField] private GameObject boxUIHuman;

    [SerializeField] GameObject chefPrefab;
    [SerializeField] GameObject chefUIPrefab;

    [SerializeField] GameObject chefCookerPrefab;
    [SerializeField] GameObject chefPreparePrefab;
    [SerializeField] GameObject dishWasherPrefab;

    Chef chef;
    ChefUI chefUI;

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
    [SerializeField] List<Ingredient> listIngredient;
    public List<Ingredient> ListIngredient => listIngredient;

    //Đánh giá
    [SerializeField] private EvaluateUI evaluateUI;

    //Bàn chờ


    //Khách hàng
    [SerializeField] private GameObject boxCustomer;
    [SerializeField] private GameObject boxCustomerUI;
    [SerializeField] float timeWaitSpawn = 5;
    private List<Customer> spawnedCustomers = new List<Customer>();
    [SerializeField] private Customer[] customerPrefabs;
    [SerializeField] GameObject CustomerUIPrefab;


    //Nồi nấu
    [SerializeField] private GameObject boxItem;
    [SerializeField] private GameObject cookItemPrefab;
    private CookItem cookItem;
    public CookItem CookItem => cookItem;

    //Tủ lạnh
    [SerializeField] private GameObject fridgeItemPrefab;
    private FridgeItem fridgeItem;
    public FridgeItem FridgeItem => fridgeItem;

    //Bồn rửa
    [SerializeField] private GameObject washItemPrefab;
    private WashItem washItem;
    public WashItem WashItem => washItem;

    //Ba lô
    InventoryHolder inventoryHolder;
    public InventoryHolder InventoryHolder { get { return inventoryHolder; } set {  inventoryHolder = value; } }

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
    void Awake()
    {
        areaState = AreaState.StartGame;
        if (day == 0) day = 1;
        LoadStartGame(day);
    }
   
    
    void Update()
    {
        if(areaState == AreaState.StartGame)
        {
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
        //loadChef
        chef = Instantiate(chefPrefab, boxHuman.transform).GetComponent<Chef>();
        chefUI = Instantiate(chefUIPrefab, boxUIHuman.transform).GetComponent<ChefUI>();

        chef.SetChefUI(chefUI);
        chefUI.SetChefHover(chef);

        chefCooker = Instantiate(chefCookerPrefab, boxHuman.transform).GetComponent<ChefCooker>();
        chefPrepare = Instantiate(chefPreparePrefab, boxHuman.transform).GetComponent<ChefPrepare>();
        dishWasher = Instantiate(dishWasherPrefab, boxHuman.transform).GetComponent<DishWasher>();

        //load bag
        inventoryHolder = gameObject.GetComponent<InventoryHolder>();

        //loadMenu
        menuByDay = MenuRestaurant[day - 1];
        menuSpecialByDay = MenuRestaurantSpecial[day - 1];

        //load nồi nấu
        cookItem = Instantiate(cookItemPrefab, boxItem.transform).GetComponent<CookItem>();

        //load tủ lạnh
        fridgeItem = Instantiate(fridgeItemPrefab, boxItem.transform).GetComponent<FridgeItem>();
        fridgeItem.SetIngredients(listIngredient);

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
            if (dinnerTable.CheckTable())
            {
                Customer randomCustomerPrefab = GetRandomCustomerPrefab();
                GameObject customerObject = Instantiate(randomCustomerPrefab.gameObject, boxCustomer.transform);
                GameObject customerUIObject = Instantiate(CustomerUIPrefab, boxCustomerUI.transform);

                Customer customerComponent = customerObject.GetComponent<Customer>();
                CustomerUIPrefab customerUI = customerUIObject.GetComponent<CustomerUIPrefab>();
                customerComponent.gameAreaManager = this;
                customerComponent.SetCustomerUIPrefab(customerUI);

                customerUI.SetCustomerHover(customerComponent);

                spawnedCustomers.Add(customerComponent);
            }
            else
            {
                yield return null;
            }

            yield return new WaitForSeconds(timeWaitSpawn);
        }
    }

    private Customer GetRandomCustomerPrefab()
    {
        int randomIndex = UnityEngine.Random.Range(0, customerPrefabs.Length);
        return customerPrefabs[randomIndex];
    }

    public void RemoveCustomer(Customer customer)
    {
        spawnedCustomers.Remove(customer);
        DinnerTable.OutTable(customer);
    }

}


