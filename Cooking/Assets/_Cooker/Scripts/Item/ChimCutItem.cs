using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChimCutItem : MonoBehaviour
{
    [SerializeField] InventoryHolder inventoryHolder;
    [SerializeField] Ingredient nuocDaiChimCanhCut;
    [SerializeField] float maxCountCD;
    float countCD;
    private void Update()
    {
        if (countCD > 0) countCD--;
        else countCD = 0;
    }
    private void OnMouseDown()
    {
        if (inventoryHolder == null) return;
        if (countCD > 0) return;
        inventoryHolder.InventorySystem.AddToInventory(nuocDaiChimCanhCut, 1);
        countCD = maxCountCD;
    }


}
