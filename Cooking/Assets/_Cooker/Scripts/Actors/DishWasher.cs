using Akka.Actor;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DishWasher : MonoBehaviour
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
class DishWasherActor : ReceiveActor
{
    public static Props Props()
    {
        return Akka.Actor.Props.Create(() => new DishWasherActor());
    }

    public DishWasherActor()
    {
        Receive<ActorAction>(message =>
        {
            switch (message)
            {
                
            }
        });
        
    }
}