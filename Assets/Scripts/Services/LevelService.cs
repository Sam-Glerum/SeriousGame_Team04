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

    public LevelModule currentLevelModule;


    public int CurrentLevel
    {
        get
        {
            int level = 1;

            try
            {
                level = FileStorage.GetStoredData<int>(levelStorage);
            }
            catch (System.IO.FileNotFoundException exception)
            {
                // Level not stored, so it must be 1
            }

            return level;
        }

        private set
        {
            FileStorage.StoreData<int>(levelStorage, value);
        }
    }

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

    public LevelModule GetCurrentModule()
    {
        return currentLevelModule;
    }

    public double GetAvaiableTimeInSeconds()
    {
        return (startTime - new DateTime()).Seconds;
    }

    private List<LevelModuleData> GetLeftModules()
    {
        List<LevelModuleData> currentLevelModules = levelData.getLevel(CurrentLevel);
        int currentIndex = currentLevelModules.FindIndex((moduleData) =>
        {
            return (moduleData.GetShortVersion() != currentLevelModule) ||
              (moduleData.GetLongVersion() != currentLevelModule);
        });

        return currentLevelModules.GetRange(0, currentLevelModules.Count - 1);
    }

    public LevelModule GoToNextModule()
    {
        // Run AI to get next module
        List<LevelModule> modules = solverFactory
            .makeSolver(solverMethod)
            .solve(
                10,
                GetLeftModules()
            );

        // Update currentLevelModule
        currentLevelModule = modules[0];

        // If level completed
        if (currentLevelModule == null)
        {
            // Increment level
            CurrentLevel = CurrentLevel + 1;
        }

        return currentLevelModule;
    }
}
