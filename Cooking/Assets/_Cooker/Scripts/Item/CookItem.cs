using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookItem : MonoBehaviour
{
    [SerializeField] Food foodFinish;
    
    public Food GetDishFinish()
    {
        return foodFinish;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        
        if (collision.gameObject.tag == "Player"&&Input.GetKeyDown(KeyCode.E))
        {
            CookUI._instance.statusCookUI?.Invoke(true);
        }
    }
    
}
