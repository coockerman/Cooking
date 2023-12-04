using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class InventorySlot_UIFinish : MonoBehaviour
{
    public static InventorySlot_UIFinish Instance;
    public UnityEvent GetFinishFood;

    [SerializeField] GameAreaManager gameAreaManager; 
    [SerializeField] InventoryHolder inventoryHolderSetup;
    [SerializeField] Image itemSprite;
    [SerializeField] Food foodFinish;

    private Button button;
    private Chef chef;
    private Menu menuFood;

    
    private void Awake()
    {
        ClearSlot();
        Instance = this;
        button = GetComponent<Button>();
    }
    private void OnEnable()
    {
        button.onClick.RemoveAllListeners();
    }
    private void Start()
    {
        chef = gameAreaManager.Chef;
        menuFood = gameAreaManager.MenuByDay;
    }
    public void Init(Food slot)
    {
        UpdateUISlot(slot);
    }
    public void UpdateUISlot(Food slot)
    {
        if (slot != null)
        {
            itemSprite.sprite = slot.SpriteFood;
            itemSprite.color = Color.white;
        }
        else
        {
            ClearSlot();
        }

    }
    
    public void ClearSlot()
    {
        itemSprite.sprite = null;
        itemSprite.color = Color.clear;
    }
    public void CheckMenuCook()
    {
        Food foodCheck = inventoryHolderSetup.InventorySystem.CheckFood(menuFood);
        if(foodCheck!=null)
        {
            foodFinish = foodCheck;
            UpdateUISlot(foodCheck);
            button.onClick.RemoveAllListeners();
            button?.onClick.AddListener(OnUISlotClick);

        }
        else
        {
            ClearSlot();
        }
    }
    public void OnUISlotClick()
    {
        bool isDelete = inventoryHolderSetup.InventorySystem.DeleteItemSlot(foodFinish);
        if(isDelete)
        {
            if(chef.BringFood(foodFinish))
            {
                GetFinishFood?.Invoke();
                ClearSlot();
            }
        }
        else
        {

        }
    }
}
