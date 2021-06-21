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

    LevelModule currentLevelModule;


    public int CurrentLevel
    {
        get
        {
            int level = 0;

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
            return (moduleData.GetShortVersion() == currentLevelModule) ||
              (moduleData.GetLongVersion() == currentLevelModule);
        }) + 1;


        return currentLevelModules.GetRange(currentIndex, currentLevelModules.Count - currentIndex);
    }

    public LevelModule GoToNextModule()
    {
        // TODO REMOVE
        FileStorage.StoreData<int>(levelStorage, 3);

        var x = GetLeftModules();
        int availableTime = 1000;

        // Run AI to get next module
        List<LevelModule> modules = solverFactory
            .makeSolver(solverMethod)
            .solve(
                availableTime,
                x
            );

        // If level completed
        if (modules.Count == 0)
        {
            // Increment level
            CurrentLevel++;
        }
        else {
            currentLevelModule = modules[0];
        }

        return currentLevelModule;
    }
}
