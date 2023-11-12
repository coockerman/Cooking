using System;
using Akka.Actor;
using UnityEngine;

public class Chef : MonoBehaviour
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


class ChefActor : ReceiveActor
{
    public static Props Props()
    {
        return Akka.Actor.Props.Create(() => new ChefActor());
    }

    public ChefActor()
    {
        Receive<GameAction>(message =>
        {
            switch (message)
            {
              
            }
        });
    }
}