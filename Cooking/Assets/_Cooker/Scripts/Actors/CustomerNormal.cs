using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CustomerNormal : Customer
{
    protected override void Order()
    {
        foodOrder = OnOrdering(gameAreaManager.MenuByDay);
    }
}
