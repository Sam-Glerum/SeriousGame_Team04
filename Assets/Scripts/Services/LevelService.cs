using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "LevelServiceConfig", menuName = "ScriptableObjects/LevelService", order = 1)]
public class LevelService : ScriptableObject
{
    [SerializeField]
    LevelData levelData;
    int currentLevel; // TODO : Get level from storage

    // Solver solver;

    LevelModule currentLevelModule;

    LevelModule GetCurrentModule()
    {
        return currentLevelModule;
    }

    LevelModule GoToNextModule()
    {
        // Run AI to get next module

        // Update currentLevelModule

        // If level completed, update storage

        return currentLevelModule;
    }
}
