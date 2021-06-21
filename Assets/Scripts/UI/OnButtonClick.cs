using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class OnButtonClick : MonoBehaviour
{
    [SerializeField]
    private string value;

    [SerializeField]
    private UnityEvent<string> onClicked;

    private void OnMouseDown()
    {
        onClicked.Invoke(value);
    }
}
