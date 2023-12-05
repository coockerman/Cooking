using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuItem : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Player" && Input.GetKeyDown(KeyCode.E))
        {
            MenuUI._instance.statusMenuUI?.Invoke(true);
        }
    }
}
