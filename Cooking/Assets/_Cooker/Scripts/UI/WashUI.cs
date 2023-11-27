using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WashUI : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    private bool isDragging = false;
    [SerializeField] GameObject Wash;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;

    void Start()
    {
        rectTransform = Wash.GetComponent<RectTransform>();
        canvasGroup = Wash.GetComponent<CanvasGroup>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // Bắt đầu kéo đối tượng
        isDragging = true;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Di chuyển đối tượng theo vị trí của chuột
        rectTransform.anchoredPosition += eventData.delta / canvasGroup.transform.localScale.x;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        // Kết thúc kéo đối tượng
        isDragging = false;
        canvasGroup.blocksRaycasts = true;
    }
    public void OffObj()
    {
        gameObject.SetActive(false);
    }
}
