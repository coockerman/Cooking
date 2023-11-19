using Akka.Actor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour
{
    Transform table;
    [SerializeField] float speedRun;

    private void Start()
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
    }


    public void OnOrdering(object o)
    {
        var selectedFood = (List<Menu>)o;
        //TODO: 4. Handle animation or texture when customer order food here
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
                    var someFunnyFood = selectSomeFoods.MenuToSelected.GetRange(0, 2); 
                    //TODO: 3. Random some food or create pickFood func here & replace someFunnyFood
                    Sender.Tell(new OrderFood(someFunnyFood));
                    break;
            }
        });
    }
    
}