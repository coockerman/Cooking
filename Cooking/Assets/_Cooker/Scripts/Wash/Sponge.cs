using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sponge : MonoBehaviour
{
    
    private bool isDragging = false;
    private Vector3 offset;

    private void OnMouseDown()
    {
        // Tính toán khoảng cách giữa vị trí chuột và vị trí GameObject
        offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        isDragging = true;
    }

    private void OnMouseUp()
    {
        isDragging = false;
    }

    private void Update()
    {
        Debug.Log("true");
        if (isDragging)
        {
            // Chuyển đổi vị trí chuột thành vị trí thế giới
            Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // Áp dụng offset để đảm bảo GameObject không nhảy đột ngột
            transform.position = new Vector3(cursorPosition.x + offset.x, cursorPosition.y + offset.y, transform.position.z);
            Debug.Log("A");
        }
    }
}
