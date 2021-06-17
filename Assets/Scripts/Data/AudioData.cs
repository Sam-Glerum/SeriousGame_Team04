using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AudioData : MonoBehaviour
{
    [SerializeField]
    List<AudioFragment> levelOneShort;

    [SerializeField]
    List<AudioFragment> levelOneLong;

    LevelModule getLevelOneShort()
    {
        return new LevelModule(levelOneShort);
    }

    LevelModule getLevelOneLong()
    {
        return new LevelModule(levelOneLong);
    }
}

[System.Serializable]
public class AudioFragment
{
    public AudioSource audioSource;
    public string text;

    public double GetDuration()
    {
        return audioSource.time;
    }
}

public class LevelModule
{
    List<AudioFragment> audioFragments;

    public LevelModule(List<AudioFragment> audioFragments)
    {
        this.audioFragments = audioFragments;
    }

    List<AudioFragment> GetAudioFragments()
    {
        return audioFragments;
    }

    double GetDuration()
    {
        return audioFragments.Aggregate(.0, (total, next) =>
        {
            return total + next.GetDuration();
        });
    }
}
