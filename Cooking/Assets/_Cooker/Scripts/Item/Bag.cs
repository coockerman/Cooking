using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

[Serializable]
public class ItemBag1
{
    int count;
    public int Count => count;
    Ingredient ingredient;
    public Ingredient Ingredient => ingredient;
    public ItemBag1(Ingredient ingredient)
    {
        this.ingredient = ingredient;
    }
    public ItemBag1(Ingredient ingredient, int count)
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
    
}
