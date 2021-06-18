using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
public class LevelModule
{
    public enum Type
    {
        AUDIO,
        QUIZ,
        GAME
    }

    public enum Duration
    {
        SHORT,
        LONG
    }

    [SerializeField]
    List<AudioFragment> audioFragments;

    [SerializeField]
    double duration;

    [SerializeField]
    Type type;

    public Type GetLevelModuleType()
    {
        return type;
    }

    public List<AudioFragment> GetAudioFragments()
    {
        return audioFragments;
    }

    public double GetDuration()
    {
        switch (type)
        {
            case Type.AUDIO:
                return audioFragments.Aggregate(.0, (total, next) =>
            {
                return total + next.GetDuration();
            });
            default:
                return duration;
        }
    }

}

[System.Serializable]
public class LevelModuleData
{

    [SerializeField]
    bool isRequired;

    [SerializeField]
    LevelModule shortVersion;

    [SerializeField]
    LevelModule longVersion;

    public LevelModule GetShortVersion()
    {
        return shortVersion;
    }

    public LevelModule GetLongVersion()
    {
        return longVersion;
    }

    public bool GetIsRequired()
    {
        return isRequired;
    }
}

