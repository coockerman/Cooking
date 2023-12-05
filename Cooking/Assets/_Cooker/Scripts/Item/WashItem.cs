using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WashItem : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" && Input.GetKeyDown(KeyCode.E))
        {
            WashUI._instance.statusWashUI?.Invoke(true);
        }
    }
}
