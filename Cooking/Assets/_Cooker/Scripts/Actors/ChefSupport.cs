using System;
using System.Collections.Generic;
using Akka.Actor;
using UnityEngine;

public class ChefSupport : MonoBehaviour
{
    // Start is called before the first frame update
    public void OnProcessMaterial(object data)
    {
        var menuByDay = (Dictionary<int, Menu>)data;

    }
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

class ChefSupportActor : ReceiveActor
{
    public static Props Props()
    {
        return Akka.Actor.Props.Create(() => new ChefSupportActor());
    }
    
    private void HandleStartJob(StartJob startJob)
    {
        var menuByDay = (Dictionary<int, Menu>)startJob.data;
        var chefSupport = (ChefSupport)startJob.monoBehavior;
        Action<object> callBackAction = (menu) => chefSupport.OnProcessMaterial(menuByDay);
        Context.Self.Tell(new ProcessMaterial(menuByDay, callBackAction));
    }

    private void ProcessMaterial(ProcessMaterial data)
    {
        data.CallBack(data.data as Dictionary<int, Menu>);
    }

    public ChefSupportActor()
    {
        Receive<ActorAction>(message =>
        {
            switch (message)
            {
              case StartJob actorAction:
                  HandleStartJob(actorAction);
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
}