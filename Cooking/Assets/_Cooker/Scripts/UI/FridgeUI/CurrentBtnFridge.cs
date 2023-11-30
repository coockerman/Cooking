using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CurrentBtnFridge : MonoBehaviour
{
    Ingredient ingredient;
    Button btnCurrent;
    Image img;
    private void OnEnable()
    {
        btnCurrent = GetComponent<Button>();
        img = GetComponent<Image>();
        btnCurrent.onClick.AddListener(ShowUI);
    }
    void ShowUI()
    {
        FridgeUI._instance.ingredientUI?.Invoke(ingredient);
    }
    public void SetData(Ingredient ingredient)
    {
        this.ingredient = ingredient;
        img.sprite = ingredient.SpriteIngredient;
    }
    public void CleanData()
    {
        this.ingredient = null;
    }
}
