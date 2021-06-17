using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
public class LevelModule
{
    [SerializeField]
    List<AudioFragment> audioFragments;

    [SerializeField]
    bool isRequired;

    public bool GetIsRequired()
    {
        return isRequired;
    }

    public List<AudioFragment> GetAudioFragments()
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

