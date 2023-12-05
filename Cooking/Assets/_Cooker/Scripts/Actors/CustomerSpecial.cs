using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSpecial : Customer
{
    protected override void Order()
    {
        foodOrder = OnOrdering(gameAreaManager.MenuSpecialByDay);
    }
}
