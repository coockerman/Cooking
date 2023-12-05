using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChefUI : MonoBehaviour
{
    Chef chefHover;
    [SerializeField] GameObject boxChefUI;
    [SerializeField] Image imgFood;

    private void Update()
    {
        if (chefHover != null)
        {
            transform.position = chefHover.transform.position;
        }
    }

    public void SetChefHover(Chef chef)
    {
        chefHover = chef;
    }

    public void SetStatusBoxImg(Food food)
    {
        if(food != null)
        {
            imgFood.sprite = food.SpriteFood;
            boxChefUI.SetActive(true);
            return;
        }
        boxChefUI.SetActive(false);
    }

}
