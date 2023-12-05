using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using static Akka.IO.Tcp;

public class MenuUI : MonoBehaviour
{
    public static MenuUI _instance;
    public UnityEvent<bool> statusMenuUI;

    [SerializeField] Color colorDark;
    [SerializeField] GameAreaManager gameAreaManager;
    [SerializeField] Image imgx2;
    [SerializeField] Image imgx1;
    [SerializeField] List<Image> imagesIngredient;
    [SerializeField] TextMeshProUGUI nameFood;
    [SerializeField] TextMeshProUGUI keChuyen;

    [SerializeField] GameObject UI;
    [SerializeField] Button exitBtn;

    Menu menu;
    int chose;
    private void Awake()
    {
        Initialization();
    }
    private void Start()
    {
        menu = gameAreaManager.MenuByDay;
        chose = 0;
        CleanMenu();
        SetDataFoodInMenu(menu.Foods[0]);
    }
    public void ClickNectFood()
    {
        if(chose < menu.Foods.Count - 1)
        {
            chose++;
        }
        else
        {
            chose = 0;
        }
        CleanMenu();
        SetDataFoodInMenu(menu.Foods[chose]);
    }
    public void ClickBackFood()
    {
        if (chose == 0)
        {
            return;
        }
        else
        {
            chose--;
        }
        CleanMenu();
        SetDataFoodInMenu(menu.Foods[chose]);
    }
    void SetDataFoodInMenu(Food foodChose)
    {
        imgx1.sprite = foodChose.SpriteFood;
        imgx1.color = Color.white;
        imgx2.sprite = foodChose.SpriteFood;
        imgx2.color = Color.white;
        nameFood.text = foodChose.NameFood;
        keChuyen.text = foodChose.KeChuyen;
        SetDataIngredientInFood(foodChose);
    }
    void SetDataIngredientInFood(Food foodChose)
    {
        for(int i = 0; i < foodChose.VitriIngredients.Count; i++)
        {
            imagesIngredient[i].sprite = foodChose.VitriIngredients[i].ingredient.SpriteIngredient;
            imagesIngredient[i].color = Color.white;
        }
    }
    void CleanMenu()
    {
        imgx1.sprite = null;
        imgx2.sprite = null;
        imgx1.color = Color.clear;
        imgx2.color = Color.clear;
        nameFood.text = "";
        keChuyen.text = "";
        CleanMenuIngredients();
    }
    void CleanMenuIngredients()
    {
        for(int i = 0; i< 4; i++)
        {
            imagesIngredient[i].sprite = null;
            imagesIngredient[i].color = Color.clear;
        }
    }
    void Initialization()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        exitBtn.onClick.AddListener(() => SetStatusObj(false));
        statusMenuUI.AddListener(SetStatusObj);
    }
    public void SetStatusObj(bool status)
    {
        UI.SetActive(status);
        gameAreaManager.Chef.SetIsWork(status);
    }
}
