using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSpecial : Customer
{
    
    public override Food GetFood()
    {
        return null;
    }
    protected override void Order()
    {
        foodOrder = OnOrdering(gameAreaManager.MenuSpecialByDay);
    }
}
