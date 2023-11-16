using System;
using System.Collections.Generic;
using Akka.Actor;
using Unity.Collections;
using UnityEngine;

public class ChefCooker : MonoBehaviour
{
    Food foodWorking;
    
    // Start is called before the first frame update
    public void OnProcessMaterial(object data)
    {
        var menuByDay = (Dictionary<int, Menu>)data;

    }
    public void OnCooking(object data)
    {
        if(foodWorking == null)
        {
            foodWorking = (Food)data;
        }
    }
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
    public static Props Props()
    {
        return Akka.Actor.Props.Create(() => new ChefCookerActor());
    }
    
    public ChefCookerActor()
    {
        Receive<ActorAction>(message =>
        {
            switch (message)
            {
                case StartJob actorAction:
                    HandleStartJob(actorAction);
                    break;
                case Order orderFood:
                    HandleOrderFood(orderFood);
                    break;
            }
        });
        
        Receive<GameAction>(message =>
        {
            switch (message)
            {
               case ProcessMaterial processMaterial:
                   ProcessMaterial(processMaterial);
                   break;
            }
        });
    }
    private void HandleStartJob(StartJob startJob)
    {
        var menuByDay = (Dictionary<int, Menu>)startJob.Data;
        var chefSupport = (ChefCooker)startJob.MonoBehaviour;
        Action<object> callBackAction = (menu) => chefSupport.OnProcessMaterial(menuByDay);
        Context.Self.Tell(new ProcessMaterial(menuByDay, callBackAction));
    }
    
    void HandleOrderFood(Order orderFood)
    {
        var food = (Order)orderFood.Data;
        var chefCooker = (ChefCooker)orderFood.MonoBehaviour;
        Action<object> callBackAction = (menu) => chefCooker.OnCooking(food);
        callBackAction.Invoke(chefCooker);
    }

    private void ProcessMaterial(ProcessMaterial data)
    {
        data.CallBack(data.Data as Dictionary<int, Menu>);
    }
}