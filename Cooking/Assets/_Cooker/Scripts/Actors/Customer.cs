using Akka.Actor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour
{

    private void Start()
    {
        
    }
    

    public void OnOrdering(object o)
    {
        var selectedFood = (List<Menu>)o;

    }
}


class CustomerActor : ReceiveActor
{
    public static Props Props()
    {
        return Akka.Actor.Props.Create(() => new CustomerActor());
    }

    public CustomerActor()
    {
        Receive<GameAction>(message =>
        {
            switch (message)
            {
                case SelectSomeFoods selectSomeFoods:
                    var dayNow = selectSomeFoods.dayNow;
                    var menu = selectSomeFoods.MenuToSelected.GetRange(0, 2);
                    var foodOrder = SelectRandomFood(menu[dayNow]);
                    Sender.Tell(new StartWaiting(foodOrder));
                    break;
                case StartWaiting waiting:
                    Food food = waiting.foodOrder;
                    StartWaitingFood();
                    break;
            }
        });
        
    }
    Food SelectRandomFood(Menu menuDay)
    {
        List<Food> foods = menuDay.Foods;
        if (foods == null || foods.Count == 0)
        {
            return null;
        }
        int randomIndex = UnityEngine.Random.Range(0, foods.Count);
        return foods[randomIndex];
    }
    void StartWaitingFood()
    {

    }
}