using Akka.Actor;
using System;
using System.Collections;
using UnityEngine;

public class Customer : MonoBehaviour
{
    Transform table;
    [SerializeField] float speedRun;
    Food FoodOrder;
    DinnerTable DinnerTable;
    private void Start()
    {
        DinnerTable = FindObjectOfType<DinnerTable>();
        FindTable();
    }
    void FindTable()
    {
        Table table = DinnerTable.ChoseTable();
        if(table != null )
        {
            Debug.Log("b");
            this.table = table.gameObject.transform;
            GotoTheTable();
        }
        else
        {
            GotoWait();
        }
        
    }
    void GotoTheTable()
    {
        if(table!=null)
        {
            StartCoroutine(MoveToTable());

        }
    }
    void GotoWait()
    {

    }
    IEnumerator MoveToTable()
    {
        
        Vector3 startPosition = transform.position;
        Vector3 targetPosition = table.transform.position;
        float distance = Vector3.Distance(startPosition, targetPosition);
        float travelTime = distance / speedRun;

        float elapsedTime = 0f;
        while (elapsedTime < travelTime)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / travelTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition;
        Debug.Log("Đã đến bàn ăn!");
    }
    
}


class CustomerActor : ReceiveActor
{
    private IActorRef _chefCooker = Context.ActorOf(ChefCookerActor.Props(), "ChefCookerActor");
    private IActorRef _dishWasher = Context.ActorOf(DishWasherActor.Props(), "DishWasherActor");
    private IActorRef _chefPrepare = Context.ActorOf(ChefPrepareActor.Props(), "ChefPrepareActor");

    public static Props Props()
    {
        return Akka.Actor.Props.Create(() => new CustomerActor());
    }

    public CustomerActor()
    {
        Receive<ActorAction>(message =>
        {
            switch (message)
            {
                

            }
        });
    }
    
}