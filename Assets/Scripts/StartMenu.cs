using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static SaveSystem;
using static Player;

public class StartMenu : MonoBehaviour
{
    public Player player = new Player();

    private void LoadLevel(int levelIndex)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + levelIndex);
    }

    private PlayerData LoadPlayerData(Player player)
    {
        return (PlayerData)SaveSystem.LoadData(player);
    }

    private bool SaveFileExists()
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

    // ======================
    // Unity Editor methods
    // ======================
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void OpenSettings()
    {
        SceneManager.LoadScene("SettingsMenu");
    }

    public void StartGame()
    {
        // Checks if a save file exists
        if (SaveFileExists() && LoadPlayerData(player).currentLevel > 0)
        {
            LoadLevel(LoadPlayerData(player).currentLevel);
        }
        else
        {
            // Creates a new save file if it does not yet exist
            SaveSystem.SaveData(player);
            // Loads the first level in the build settings list (after the menu scene)
            LoadLevel(1);
        }
    }
}
