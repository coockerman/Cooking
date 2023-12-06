using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChefSetLayer : MonoBehaviour
{
    [SerializeField] MeshRenderer chefMesh;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "nhanvien")
        {
            chefMesh.sortingLayerName = "Human";
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "nhanvien")
        {
            chefMesh.sortingLayerName = "HumanIn";
        }
    }
}
