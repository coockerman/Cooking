using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FridgeItem : MonoBehaviour
{
    List<Ingredient> ingredients;
    Bag bag;

    public List<Ingredient> GetIngredients()
    {
        return ingredients;
    }
    public void SetIngredients(List<Ingredient> ingredients)
    {
        this.ingredients = ingredients;
    }
    public void SetBag(Bag bag)
    {
        this.bag = bag;
    }
    public void AddToBag(Ingredient ingredient, int sl)
    {
        ItemBag bagItem = new ItemBag(ingredient);
        bag.AddItem(sl, bagItem);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Player" && Input.GetKeyDown(KeyCode.E))
        {
            FridgeUI._instance.statusFridgeUI?.Invoke(true);
        }
    }
}
