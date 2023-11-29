using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class ItemBag
{
    int count;
    public int Count => count;
    Ingredient ingredient;
    public Ingredient Ingredient => ingredient;
    public void addItem(int sl)
    {
        count += sl;
    }
    public bool getItem(int sl)
    {
        if(count-sl<0) return false;
        count -= sl;
        return true;
    }
}

public class Bag : MonoBehaviour
{
    [SerializeField] List<ItemBag> itemBags;
    public void AddItem(int sl, ItemBag item)
    {
        foreach (ItemBag itemIn in itemBags)
        {
            if (item == itemIn)
            {
                item.addItem(sl);
            }
            Debug.Log(itemIn.Ingredient.NameIngredient + ": " + itemIn.Count);
        }
    }
    public void GetItem(int sl, ItemBag item)
    {
        foreach (ItemBag itemIn in itemBags)
        {
            if (item == itemIn)
            {
                item.getItem(sl);
            }
        }
    }
}
