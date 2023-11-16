using Akka.Actor;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChefPrepare : MonoBehaviour
{
    public void OnPrepare(object data)
    {

    }
    
}


class ChefPrepareActor : ReceiveActor
{
    public static Props Props()
    {
        return Akka.Actor.Props.Create(() => new ChefPrepareActor());
    }

    public ChefPrepareActor()
    {
        Receive<ActorAction>(message =>
        {
            switch (message)
            {
                case Order orderFood:
                    HandleOrderFood(orderFood);
                    break;
            }
        });

    }
    void HandleOrderFood(Order orderFood)
    {
        var food = (Order)orderFood.Data;
        var chefPrepare = (ChefPrepare)orderFood.MonoBehaviour;
        Action<object> callBackAction = (menu) => chefPrepare.OnPrepare(food);
    }
}