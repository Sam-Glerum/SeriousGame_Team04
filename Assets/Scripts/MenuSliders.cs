using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;


[RequireComponent(typeof (Slider))]
public class MenuSliders : MonoBehaviour
{

    Slider slider
    {
        get { return GetComponent<Slider>(); }
    }

    public AudioMixer mixer;
    public string volumeSliderName;
    // Start is called before the first frame update

    public void UpdateValueOnChange(float value)
    {
        mixer.SetFloat(volumeSliderName, value);
    }
}
