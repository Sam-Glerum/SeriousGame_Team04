using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelDataConfig", menuName = "ScriptableObjects/LevelData", order = 1)]
public class LevelData : ScriptableObject
{
    [SerializeField]
    List<LevelModuleData> levelOne;

    [SerializeField]
    List<LevelModuleData> levelTwo;

    [SerializeField]
    List<LevelModuleData> levelThree;

    public List<LevelModuleData> getLevel(int level)
    {
        switch (level)
        {
            case 1:
                return levelOne;
            case 2:
                return levelOne;
            case 3:
                return levelOne;
            default:
                throw new System.Exception("Unkown level");
        }
    }
}

