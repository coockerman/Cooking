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


    public UnityEvent<bool> statusFridgeUI;
    public UnityEvent<Ingredient> ingredientUI;

    FridgeItem fridgeItem;

    bool isLoadData = false;
    private void Start()
    {
        Initialization();
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
        if(status && !isLoadData)
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
        imgThuHoach.sprite = ingredient.ThuHoachSprite;
        imgThuHoach.color = Color.white;
        imgNguyenLieu.sprite = ingredient.SpriteIngredient;
        imgNguyenLieu.color = Color.white;
        nameIngredient.text = ingredient.NameIngredient;
        btnGetIngredient.onClick.RemoveAllListeners();
        btnGetIngredient.onClick.AddListener(() => AddToBag(ingredient, 1));
    }
    void AddToBag(Ingredient ingredient, int sl)
    {
        gameAreaManager.InventoryHolder.InventorySystem.AddToInventory(ingredient, sl);
        
    }
    void CleanFridgeUI()
    {
        imgThuHoach.sprite = null;
        imgThuHoach.color = Color.clear;
        imgNguyenLieu.sprite = null;
        imgNguyenLieu.color = Color.clear;
        nameIngredient.text = "";
        thongtin1.text = "";
        thongtin2.text = "";

    }
}
