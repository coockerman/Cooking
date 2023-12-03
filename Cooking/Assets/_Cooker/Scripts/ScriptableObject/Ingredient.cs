using UnityEngine;

[CreateAssetMenu(fileName = "Ingredient", menuName = "ScriptableObjects/Ingredient", order = 2)]
public class Ingredient : GameScriptableObject
{
    public string NameIngredient;
    public IngredientStatus Status;
    public Sprite SpriteIngredient;
    public Sprite ThuHoachSprite;
    public int MaxStackSize;
    
}
