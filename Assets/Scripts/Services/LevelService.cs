using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "LevelServiceConfig", menuName = "ScriptableObjects/LevelService", order = 1)]
public class LevelService : ScriptableObject
{
    [SerializeField]
    LevelData levelData;

    [SerializeField]
    SolverMethod solverMethod;
    SolverFactory solverFactory = new SolverFactory();

    int currentLevel; // TODO : Get level from storage

    LevelModule currentLevelModule;

    /// <summary>
    /// Time the performance will begin and the game must have ended
    /// </summary>
    DateTime startTime;

    LevelModule GetCurrentModule()
    {
        return currentLevelModule;
    }

    public double GetAvaiableTimeInSeconds()
    {
        return (startTime - new DateTime()).Seconds;
    }

    LevelModule GoToNextModule()
    {
        // Run AI to get next module
        List<LevelModule> modules = solverFactory
            .makeSolver(solverMethod)
            .solve(
                GetAvaiableTimeInSeconds(),
                levelData.getLevel(currentLevel)
            );

        // Update currentLevelModule
        currentLevelModule = modules[0];

        // If level completed
        if (currentLevelModule == null)
        {
            // TODO : update storage
        }

        return currentLevelModule;
    }
}
