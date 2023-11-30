using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

[Serializable]
public class ItemBag
{
    int count;
    public int Count => count;
    Ingredient ingredient;
    public Ingredient Ingredient => ingredient;
    public ItemBag(Ingredient ingredient)
    {
        this.ingredient = ingredient;
    }
    public ItemBag(Ingredient ingredient, int count)
    {
        this.count = count;
        this.ingredient = ingredient;
    }
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
    [SerializeField]List<ItemBag> itemBags;
    public void SetUpItemBag(List<Ingredient> ingredients)
    {
        foreach(Ingredient ingredient in ingredients)
        {
            ItemBag item = new ItemBag(ingredient, 0);
            itemBags.Add(item);
        }
    }
    public void AddItem(int sl, ItemBag item)
    {
        foreach (ItemBag itemIn in itemBags)
        {
            if (item == itemIn)
            {
                int t = itemBags.IndexOf(itemIn);
                itemBags[t].addItem(sl);
                //itemIn.addItem(sl);
            }
        }
        ShowItemBagDebug();
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
    public void ShowItemBagDebug()
    {
        foreach(ItemBag itemIn in itemBags)
        {
            Debug.Log(itemIn.Ingredient.NameIngredient + ": " + itemIn.Count);
        }
    }
}
