using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Collider))]
public class ClickHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler // required interface when using the OnPointerDown method.
{

    public UnityEvent UpEvent;
    public UnityEvent DownEvent;


    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Down");
        DownEvent?.Invoke();
    }



    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("Up");
        UpEvent?.Invoke();
    }
}
