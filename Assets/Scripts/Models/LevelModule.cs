using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEditor;
using MyBox;
using System;

[System.Serializable]
[CustomPropertyDrawer(typeof(ConditionalFieldAttribute))]
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

    [Serializable]
    public struct Answers
    {
        public string answer;
        public bool validAnswer;
    }

    [SerializeField]
    [ConditionalField("type", false, Type.QUIZ)] public Answers answers; 
    [SerializeField]
    [ConditionalField("type", false, Type.QUIZ)] public string question;
    [SerializeField]
    [ConditionalField("type", false, Type.QUIZ)] public AudioClip questionExplanation;

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

