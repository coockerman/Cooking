using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum ChefState
{
    bringFood,
    work,
    rest,
    endTime
}

public class Chef : MonoBehaviour
{
    [SerializeField] Food foodBring;
    ChefMovement chefMovement;
    ChefUI chefUI;
    ChefState chefState;


    private void Start()
    {
        chefMovement = GetComponent<ChefMovement>();
    }

    private void Update()
    {
        if(chefState==ChefState.bringFood)
        {

        }
        else if(chefState == ChefState.work)
        {
            if(chefMovement.IsWork == false)
            {
                chefMovement.IsWork = true;
            }
        }
        else if(chefState == ChefState.rest)
        {
            if (chefMovement.IsWork == true)
            {
                chefMovement.IsWork = false;
            }
        }
        else if (chefState == ChefState.endTime)
        {
            if (chefMovement.IsWork == false)
            {
                chefMovement.IsWork = true;
            }
        }

        
    }
    public void SetChefUI(ChefUI chefUI)
    {
        this.chefUI = chefUI;
        this.chefUI.SetStatusBoxImg(foodBring);
    }
    public void BringFood(Food food)
    {
        if(chefState == ChefState.rest)
        {
            foodBring = food;
            chefUI.SetStatusBoxImg(foodBring);
        }
    }

    public Food ServeFood()
    {
        Food food = foodBring;
        foodBring = null;
        chefUI.SetStatusBoxImg(foodBring);
        chefState = ChefState.rest;
        return food;
    }
    public void SetIsWork(bool isWork)
    {
        if(isWork)
        {
            chefState = ChefState.work;
        }else if(!isWork)
        {
            chefState = ChefState.rest;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "CookItem")
        {
            if (chefState != ChefState.rest) return;
            foodBring = collision.GetComponent<CookItem>().GetDishFinish();
            chefState = ChefState.bringFood;
        }

    }
}
