using Akka.Actor;
using UnityEngine;

public class Greet
{
    public string Who { get; private set; }

    public Greet(string who)
    {
        Who = who;
    }
}

public class GreetingActor : ReceiveActor
{
    public GreetingActor()
    {
        // Tell the actor to respond to the Greet message
        Receive<Greet>(greet => Debug.Log("Hello " + greet.Who));
    }
}

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Hello from C#");

        var system = ActorSystem.Create("MySystem");
        var greeter = system.ActorOf<GreetingActor>("greeter");
        greeter.Tell(new Greet("World"));
        Debug.Log("Hello from C# 2222  2 times");
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Enter update");
    }
}
