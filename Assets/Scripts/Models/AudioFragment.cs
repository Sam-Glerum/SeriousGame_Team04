using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AudioFragment
{
    public AudioSource audioSource;
    public string text;
    public bool isRequired;

    public AudioSource GetAudioSource()
    {
        return audioSource;
    }

    public string GetText()
    {
        return text;
    }

    public bool GetIsRequired()
    {
        return isRequired;
    }

    public double GetDuration()
    {
        return audioSource.time;
    }
}
