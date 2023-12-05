using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class FridgeUI : MonoBehaviour
{
    [SerializeField] GameAreaManager gameAreaManager;
    public static FridgeUI _instance;
    [SerializeField] TextAreaAttribute textAreaAttribute;
    [SerializeField] GameObject UIBtn;

    [SerializeField] GameObject BoxCurrentBtn;
    [SerializeField] GameObject currentBtnIngredientPrefab;

    [SerializeField] Button exitBtn;
    [SerializeField] Image imgThuHoach;
    [SerializeField] Image imgNguyenLieu;
    [SerializeField] Button btnGetIngredient;
    [SerializeField] TextMeshProUGUI nameIngredient;
    [SerializeField] TextMeshProUGUI thongtin1;
    [SerializeField] TextMeshProUGUI thongtin2;
    [SerializeField] TextMeshProUGUI notificationText;
    [SerializeField] float CDmaxFridge;
    float CDFridge;

    public UnityEvent<bool> statusFridgeUI;
    public UnityEvent<Ingredient> ingredientUI;

    FridgeItem fridgeItem;

    bool isLoadData = false;
    
    private void Start()
    {
        Initialization();
    }
    private void Update()
    {
        UpdateCD();
    }
    void UpdateCD()
    {
        if (CDFridge < 0)
        {
            CDFridge = 0;
        }
        else if (CDFridge > 0)
        {
            CDFridge -= Time.deltaTime;
        }
    }
    void Initialization()
    {
        if (_instance == null)
        {
            _instance = this;
        }

        exitBtn.onClick.AddListener(() => SetStatusObj(false));
        statusFridgeUI.AddListener(SetStatusObj);
        ingredientUI.AddListener(SetDataCurrent);

        fridgeItem = gameAreaManager.FridgeItem;
    }
    public void SetStatusObj(bool status)
    {
        UIBtn.SetActive(status);
        gameAreaManager.Chef.SetIsWork(status);
        CleanFridgeUI();
        if (status && !isLoadData)
        {
            StartCoroutine(LoadFridgeUIWithDelay());
            isLoadData = true;
        }
    }
    public IEnumerator LoadFridgeUIWithDelay()
    {
        CleanFridgeUI();

        // Add a delay of 2 seconds
        yield return new WaitForSeconds(0.1f);

        List<Ingredient> ingredients = fridgeItem.GetIngredients();
        foreach (Ingredient current in ingredients)
        {
            GameObject currentBtnObj = Instantiate(currentBtnIngredientPrefab, BoxCurrentBtn.transform);
            CurrentBtnFridge currentBtn = currentBtnObj.GetComponent<CurrentBtnFridge>();
            currentBtn.SetData(current);
        }
    }


    void SetDataCurrent(Ingredient ingredient)
    {
        CleanFridgeUI();
        imgThuHoach.sprite = ingredient.ThuHoachSprite;
        imgThuHoach.color = Color.white;
        imgNguyenLieu.sprite = ingredient.SpriteIngredient;
        imgNguyenLieu.color = Color.white;
        nameIngredient.text = ingredient.NameIngredient;
        thongtin1.text = ingredient.ThongTin1;
        thongtin2.text = ingredient.ThongTin2;
        btnGetIngredient.onClick.AddListener(() => AddToBag(ingredient, 1));
    }
    void AddToBag(Ingredient ingredient, int sl)
    {
        if(CDFridge != 0)
        {
            notificationText.text = "CD còn: " + (int)CDFridge + "s";
            notificationText.color = Color.blue;
            return;
        }
        bool check = gameAreaManager.InventoryHolder.InventorySystem.AddToInventory(ingredient, sl);
        if(check)
        {
            notificationText.text = "Thêm thành công";
            notificationText.color = Color.green;
            CDFridge = CDmaxFridge;
        }
        else
        {
            notificationText.text = "Túi đồ đã đầy";
            notificationText.color = Color.red;
        }
        
    }
    void CleanFridgeUI()
    {
        btnGetIngredient.onClick.RemoveAllListeners();
        imgThuHoach.sprite = null;
        imgThuHoach.color = Color.clear;
        imgNguyenLieu.sprite = null;
        imgNguyenLieu.color = Color.clear;
        nameIngredient.text = "";
        thongtin1.text = "";
        thongtin2.text = "";
        notificationText.text = "";
        notificationText.color = Color.clear;
    }
}
