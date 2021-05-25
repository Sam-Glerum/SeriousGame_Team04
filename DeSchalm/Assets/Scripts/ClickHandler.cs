using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class ClickHandler : MonoBehaviour
{
    public UnityEvent UpEvent;
    public UnityEvent DownEvent;

    private void OnMouseDown()
    {
        Debug.Log("Down");
        DownEvent?.Invoke();
    }

    private void OnMouseUp()
    {
        Debug.Log("Up");
        UpEvent?.Invoke();
    }
}
