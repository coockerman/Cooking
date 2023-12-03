using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventorySlot
{
    [SerializeField] Ingredient itemData;
    [SerializeField] int stackSize;

    public Ingredient ItemData => itemData;
    public int StackSize => stackSize;
    public InventorySlot(Ingredient itemData, int stackSize)
    {
        this.itemData = itemData;
        this.stackSize = stackSize;
    }
    public InventorySlot()
    {
        ClearSlot();
    }
    public void ClearSlot()
    {
        this.itemData = null;
        this.stackSize = -1;
    }
    public void AssignItem(InventorySlot invSlot)
    {
        if(itemData == invSlot.ItemData)
        {
            AddToStack(invSlot.stackSize);
        }
        else
        {
            itemData = invSlot.ItemData;
            stackSize = 0;
            AddToStack(invSlot.stackSize);
        }
    }
    public void UpdateInventorySlot(Ingredient data, int amount)
    {
        itemData = data;
        stackSize = amount;
    }
    public bool EnoughRoomLeftInStack(int amountToAdd, out int amountRemaining)
    {
        amountRemaining = ItemData.MaxStackSize - stackSize;
        return EnoughRoomLeftInStack(amountToAdd);
    }
    public bool EnoughRoomLeftInStack(int amountToAdd)
    {
        
        if (itemData == null || itemData != null && stackSize + amountToAdd <= itemData.MaxStackSize)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public void AddToStack(int amount)
    {
        stackSize += amount;
    }
    public void RemoveFromStack(int amount)
    {
        stackSize -= amount;
    }
    public bool SplitStack(out InventorySlot splitStack)
    {
        if(stackSize <= 1)
        {
            splitStack = null;
            return false;
        }
        int halfStack = Mathf.RoundToInt(stackSize / 2);
        RemoveFromStack(halfStack);
        splitStack = new InventorySlot(itemData, halfStack);
        return true;
    }
}
