using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FridgeItem : MonoBehaviour
{
    List<Ingredient> ingredients;

    public List<Ingredient> GetIngredients()
    {
        return ingredients;
    }
    public void SetIngredients(List<Ingredient> ingredients)
    {
        this.ingredients = ingredients;
    }
    
    
    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Player" && Input.GetKeyDown(KeyCode.E))
        {
            FridgeUI._instance.statusFridgeUI?.Invoke(true);
        }
    }
}
