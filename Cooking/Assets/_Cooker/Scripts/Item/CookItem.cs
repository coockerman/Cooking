using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookItem : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            CookUI._instance.statusCookUI?.Invoke(true);
        }
    }
}
