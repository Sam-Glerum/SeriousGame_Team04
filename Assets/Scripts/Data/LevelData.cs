using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelDataConfig", menuName = "ScriptableObjects/LevelData", order = 1)]
public class LevelData : ScriptableObject
{
    [SerializeField]
    List<LevelModule> levelOne;

    public List<LevelModule> getLevelOne()
    {
        return levelOne;
    }
}

