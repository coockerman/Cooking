using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using static Spine.Unity.Examples.SpineboyFootplanter;

[System.Serializable]

public class InventorySystem
{
    [SerializeField] List<InventorySlot> inventorySlots;
    public List<InventorySlot> InventorySlots => inventorySlots;
    public int InventorySize => inventorySlots.Count;

    public UnityAction<InventorySlot> OnInventorySlotChanged;

    public InventorySystem(int size)
    {
        inventorySlots = new List<InventorySlot>(size);

        for(int i = 0; i < size; i++)
        {
            inventorySlots.Add(new InventorySlot());
        }
    }
    public bool AddToInventory(Ingredient itemToAdd, int amountToAdd)
    {
        if(ContainsItem(itemToAdd, out List<InventorySlot> invSlot))
        {
            foreach(var slot in invSlot)
            {
                if(slot.EnoughRoomLeftInStack(amountToAdd))
                {
                    slot.AddToStack(amountToAdd);
                    OnInventorySlotChanged?.Invoke(slot);
                    return true;
                }
            }
        }
        if(HasFreeSlot(out InventorySlot freeSlot)) {
            freeSlot.UpdateInventorySlot(itemToAdd, amountToAdd);
            OnInventorySlotChanged?.Invoke(freeSlot);
            return true;
        }
        
        return false;
    }
    public bool ContainsItem(Ingredient itemToAdd, out List<InventorySlot> invSlot)
    {
        invSlot = InventorySlots.Where(i => i.ItemData == itemToAdd).ToList();
        return invSlot == null ? false : true;
    }
    public bool HasFreeSlot(out InventorySlot freeSlot)
    {
        freeSlot = InventorySlots.FirstOrDefault(i => i.ItemData == null);
        return freeSlot == null ? false : true;
    }
    public bool DeleteItemSlot(Food food)
    {
        List<ViTriIngredient> ingredientInFood = food.VitriIngredients;
        if (InventorySlots == null) return false;
        
        foreach (ViTriIngredient inventorySlot in ingredientInFood)
        {
            int vitri = inventorySlot.vitri;
            inventorySlots[vitri].RemoveFromStack(1);
        }
        return true;
    }
    public Food CheckFood(Menu menuFood)
    {
        List<Food> foodInMenu = menuFood.Foods;
        foreach(Food item in foodInMenu)
        {
            List<ViTriIngredient> ingredientInFood = item.VitriIngredients;
            bool isTrueCongThuc = true;
            foreach (ViTriIngredient inventorySlot in ingredientInFood)
            {
                int vitri = inventorySlot.vitri;
                if (inventorySlot.ingredient != inventorySlots[vitri].ItemData) isTrueCongThuc = false;
            }
            if (isTrueCongThuc) return item;
        }
        return null;
    }
}
