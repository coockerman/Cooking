using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookItem : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        
        if (collision.gameObject.tag == "Player"&&Input.GetKeyDown(KeyCode.E))
        {
            CookUI._instance.statusCookUI?.Invoke(true);
        }
    }
    
}
