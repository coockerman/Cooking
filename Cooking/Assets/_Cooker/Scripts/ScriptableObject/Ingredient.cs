using UnityEngine;

[CreateAssetMenu(fileName = "Ingredient", menuName = "ScriptableObjects/Ingredient", order = 2)]
public class Ingredient : GameScriptableObject
{
    [SerializeField] private string nameIngredient;
    [SerializeField] private IngredientStatus status;
    [SerializeField] private Sprite spriteIngredient;
    public string NameIngredient => nameIngredient;
    public IngredientStatus Status => status;
    public Sprite SpriteIngredient => spriteIngredient;
}
