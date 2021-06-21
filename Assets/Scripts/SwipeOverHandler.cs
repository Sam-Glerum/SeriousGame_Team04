using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SwipeOverHandler : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Hover down");
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Hover start");
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Hover stop");
    }
}