using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AudioFragment
{
    public AudioClip audioClip;
    public string text;

    public AudioClip GetAudioClip()
    {
        return audioClip;
    }

    public string GetText()
    {
        return text;
    }

    public double GetDuration()
    {
        return audioClip.length;
    }
}
