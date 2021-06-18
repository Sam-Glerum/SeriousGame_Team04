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

    [SerializeField]
    string levelStorage;

    public int currentLevel
    {
        get
        {
            return FileStorage.GetStoredData<int>(levelStorage) ?? 1;
        }

        set
        {
            FileStorage.StoreData<int>(levelStorage, value);
        }
    }

    LevelModule currentLevelModule;

    /// <summary>
    /// Time the performance will begin and the game must have ended
    /// </summary>
    DateTime startTime;
    public DateTime StartTime
    {
        get
        {
            return this.startTime;
        }
        set
        {
            this.startTime = value;
        }
    }


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
            // Increment level
            currentLevel = currentLevel + 1;
        }

        return currentLevelModule;
    }
}
