using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticInventoryDisplay : InventoryDisplay
{
    [SerializeField] InventoryHolder inventoryHolder;
    [SerializeField] InventorySlot_UI[] slots;
    protected override void Start()
    {
        base.Start();
        if(inventoryHolder != null)
        {
            inventorySystem = inventoryHolder.InventorySystem;
            inventorySystem.OnInventorySlotChanged += UpdateSlot;
        }
        else
        {
            Debug.LogWarning("sos");
        }
        AssignSlot(InventorySystem);
    }
    public override void AssignSlot(InventorySystem invToDisplay)
    {
        slotDictionary = new Dictionary<InventorySlot_UI, InventorySlot>();


        for(int i = 0; i < inventorySystem.InventorySize; i++)
        {
            slotDictionary.Add(slots[i], inventorySystem.InventorySlots[i]);
            slots[i].Init(inventorySystem.InventorySlots[i]);
        }
    }
}
