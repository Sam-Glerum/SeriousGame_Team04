using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AudioFragment
{
    public AudioClip audioSource;
    public string text;

    public AudioClip GetAudioSource()
    {
        return audioSource;
    }

    public string GetText()
    {
        return text;
    }

    public double GetDuration()
    {
        return audioSource.length;
    }
}
