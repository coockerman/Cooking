using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Stain : MonoBehaviour
{
    [SerializeField] WashUI washUI;
    bool isStain;
    public bool IsStain { get { return isStain; } set { isStain = value; } }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "sponge")
        {
            washUI.DragStain(this);
        }
    }
}
