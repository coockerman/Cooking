using Spine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    public Food FoodBring { get { return foodBring; } }

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
    public bool BringFood(Food food)
    {
        if(foodBring == null && food != null)
        {
            foodBring = food;
            chefUI.SetStatusBoxImg(foodBring);
            return true;
        }
        return false;
    }

    public Food ServeFood()
    {
        if (foodBring == null) return null;
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
    
}
