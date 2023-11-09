using Akka.Actor;
using UnityEngine;

public class GameAreaManager : MonoBehaviour
{
    [SerializeField] private int customerEachRound;
    [SerializeField] private int totalChefs;
    [SerializeField] private int totalChefSupporter;
    [SerializeField] private int totalDishWasher;
    [SerializeField] private int totalWaiter;
    
    void Start()
    {
        var gameActorSystem = ActorSystem.Create("CookGameActorSystem");
        var gameManager = gameActorSystem.ActorOf(GameAreaManagerActor.Props(), "GameManagerActor");
        gameManager.Tell(new InitializeGame(customerEachRound, totalChefs, totalChefSupporter, totalDishWasher, totalWaiter, OnInitializeGame));
    }

    private void OnInitializeGame(object o)
    {
        var result = (Ingredient)o;
        //Handle animation, sound, vfx,... here
    }
    
    void Update()
    {
        
    }
}

class GameAreaManagerActor : ReceiveActor
{
    public static Props Props()
    {
        return Akka.Actor.Props.Create(() => new GameAreaManagerActor());
    }
    private void InitGameData(InitializeGame initData)
    {
        var sendBackData = new Ingredient("Strawberry", new RawIngredient());
        initData.CallBack(sendBackData);
    }

    private void HandleAutoMode()
    {

    }

    private void HandleManualMode()
    {

    }

    public GameAreaManagerActor()
    {
        Receive<GameAction>(message =>
        {
            switch (message)
            {
                case InitializeGame initData:
                    InitGameData(initData);
                    break;
                case StartGame _:

                    break;
                case SwitchToAutoMode _:
                    HandleAutoMode();
                    break;
                case SwitchToManualMode _:
                    HandleManualMode();
                    break;
            }
        });
    }
}
