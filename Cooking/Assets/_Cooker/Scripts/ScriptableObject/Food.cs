using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Food", menuName = "ScriptableObjects/Food", order = 1)]
public class Food : GameScriptableObject
{
    [SerializeField] private string nameFood;
    [SerializeField] private int cookingTime;
    [SerializeField] private int eatingTime;
    [SerializeField] private List<Ingredient> ingredients;
    [SerializeField] private Sprite spriteFood;

    public string NameFood => nameFood;
    public int CookingTime => cookingTime;
    public int EatingTime => eatingTime;
    public List<Ingredient> Ingredients => ingredients;
    public Sprite SpriteFood => spriteFood;
}
