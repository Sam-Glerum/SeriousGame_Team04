using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{

    public void setLargeText(string text)
    {

        TMP_Text largeText = GameObject.Find("LargeText").GetComponent<TMP_Text>(); ;

        if (largeText == null)
        {
            Debug.Log("Error: LargeText instance is null!");
            return;
        }
        else
        {
            largeText.text = text;
        }

    }

    public void setImageTexture(Texture texture)
    {
        RawImage largeText = GameObject.Find("Albert").GetComponent<RawImage>(); ;

        if (largeText == null)
        {
            Debug.Log("Error: Albert instance is null!");
            return;
        }
        else
        {
            largeText.texture = texture;
        }
    }
}