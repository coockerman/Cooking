using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Menu", menuName = "ScriptableObjects/Menu", order = 3)]
public class Menu : GameScriptableObject
{
    [SerializeField] private List<Food> foods;
    [SerializeField] private int day;
    public List<Food> Foods => foods;
    public int Day => day;
}