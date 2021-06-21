using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader
{
    public void LoadLevel(int levelIndex)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + levelIndex);
    }

    public void LoadSceneByName(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void LoadHomeScreen()
    {
        LoadSceneByName("Homescreen");
    }

    public PlayerData LoadPlayerData(Player player)
    {
        return (PlayerData)SaveSystem.LoadData(player);
    }

    public bool SaveFileExists(Player player)
    {
        if (SaveSystem.LoadData(player) != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
