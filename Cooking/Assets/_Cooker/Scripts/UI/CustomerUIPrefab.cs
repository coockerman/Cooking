using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomerUIPrefab : MonoBehaviour
{
    Customer customerHover;
    [SerializeField] GameObject BoxImgDish;
    [SerializeField] Image imgDish;
    [SerializeField] Slider WaitingUI;
    [SerializeField] Slider EatingUI;
    private void Update()
    {
        transform.position = customerHover.transform.position;
    }
    public void SetImgDish(Food food)
    {
        imgDish.sprite = food.SpriteFood;
    }
    public void SetCustomerHover(Customer customer)
    {
        this.customerHover = customer;
    }
    public void SetStatusBoxImgDish(bool status, Food food)
    {
        SetImgDish(food);
        BoxImgDish.SetActive(status);
    }
    public void ChangeStatusEat()
    {
        WaitingUI.gameObject.SetActive(false);
        EatingUI.gameObject.SetActive(true);
    }
    public void ChangeCountWaiting(float count)
    {
        WaitingUI.value = count;
    }
    public void ChangeCountEating(float count)
    {
        EatingUI.value = count;
    }
}
