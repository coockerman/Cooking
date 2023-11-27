using Akka.Actor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CustomerState
{
    FindTable,
    Order,
    WaitingFood,
    EatingFood,
    Evalute,
    OutTable
}

public abstract class Customer : MonoBehaviour
{
    public GameAreaManager gameAreaManager;

    protected Food foodOrder;
    protected CustomerState customerState;

    private float moveDuration = 2f;

    private void Start()
    {
        customerState = CustomerState.FindTable;
    }
    private void Update()
    {
        if(customerState == CustomerState.FindTable)
        {
            Table table = FindTable();
            StartCoroutine(MoveObject(table.gameObject.transform));
            customerState = CustomerState.Order;
        }
        else if (customerState == CustomerState.Order)
        {
            Order();
            customerState = CustomerState.WaitingFood;
        }
        else if(customerState == CustomerState.WaitingFood)
        {
            
        }
        else if(customerState == CustomerState.EatingFood)
        {

            customerState = CustomerState.Evalute;
        }
        else if (customerState == CustomerState.Evalute)
        {

            customerState = CustomerState.OutTable;
        }
        else if(customerState == CustomerState.OutTable)
        {
            OutTable();
            StartCoroutine(MoveObject(gameAreaManager.DoorOut.transform));
            Destroy(gameObject);
        }
    }
    protected virtual Table FindTable()
    {
        return gameAreaManager.DinnerTable.ChoseTable(this);
    }
    protected abstract void Order();
    protected virtual Food OnOrdering(Menu menuDay)
    {
        Menu menu = menuDay;
        if (menu.Foods.Count > 0)
        {
            int randomIndex = Random.Range(0, menu.Foods.Count);
            Food randomFood = menu.Foods[randomIndex];

            return randomFood;
        }
        else
        {
            Debug.Log("No food available in the menu.");
            return null;
        }
    }
    public abstract Food GetFood();
    protected virtual void OutTable()
    {
        gameAreaManager.DinnerTable.OutTable(this);
    }
    private IEnumerator MoveObject(Transform endPos)
    {
        float elapsedTime = 0f;

        Vector3 startPosition = transform.position;
        Vector3 endPosition = endPos.position;

        while (elapsedTime < moveDuration)
        {
            transform.position = Vector3.Lerp(startPosition, endPosition, elapsedTime / moveDuration);

            elapsedTime += Time.deltaTime;

            yield return null;
        }

        transform.position = endPosition;

    }
}


//class CustomerActor : ReceiveActor
//{
//    public static Props Props()
//    {
//        return Akka.Actor.Props.Create(() => new CustomerActor());
//    }

//    public CustomerActor()
//    {
//        Receive<GameAction>(message =>
//        {
//            switch (message)
//            {
//                case SelectSomeFoods selectSomeFoods:
//                    var dayNow = selectSomeFoods.dayNow;
//                    var menu = selectSomeFoods.MenuToSelected.GetRange(0, 2);
//                    var foodOrder = SelectRandomFood(menu[dayNow]);
//                    Sender.Tell(new StartWaiting(foodOrder));
//                    break;
//                case StartWaiting waiting:
//                    Food food = waiting.foodOrder;
//                    StartWaitingFood();
//                    break;
//            }
//        });
        
//    }
//    Food SelectRandomFood(Menu menuDay)
//    {
//        List<Food> foods = menuDay.Foods;
//        if (foods == null || foods.Count == 0)
//        {
//            return null;
//        }
//        int randomIndex = UnityEngine.Random.Range(0, foods.Count);
//        return foods[randomIndex];
//    }
//    void StartWaitingFood()
//    {

//    }
//}