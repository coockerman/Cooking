using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Akka.Actor;
using Unity.VisualScripting;
using UnityEngine;

public class ChefCooker : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}


class ChefCookerActor : ReceiveActor
{
    private readonly int _maxWaitingPrepareIngredientsTime = 5;
    public static Props Props()
    {
        return Akka.Actor.Props.Create(() => new ChefCookerActor());
    }

    public ChefCookerActor()
    {
        Receive<GameAction>(message =>
        {
            switch (message)
            {
                case OrderFood orderFood:
                    var menuRequests = orderFood.OrderList;
                    var neededIngredients =
                        (from menu in menuRequests
                        from food in menu.Foods
                        from ingredient in food.Ingredients
                        select ingredient).ToList();
                    
                    var requestIngredients = new List<Task> { Sender.Ask(new PrepareIngredients(neededIngredients), TimeSpan.FromSeconds(_maxWaitingPrepareIngredientsTime)) };
                    Task.WhenAll(requestIngredients).PipeTo(Self, Sender);
                    break;
            };
        });
    }
}