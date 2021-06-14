using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static SaveSystem;
using static Player;

public class StartMenu : MonoBehaviour
{
    public Player player = new Player();

    public void StartGame()
    {
        // Checks if a save file exists
        if (SaveSystem.LoadData(player) != null)
        {
            if (player.currentLevel > 0)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + player.currentLevel);
            }
        }
        else
        {
            // Creates a new save file if it does not yet exist
            SaveSystem.SaveData(player);
            // Loads the first level in the build settings list (after the menu scene)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void OpenSettings()
    {
        SceneManager.LoadScene("SettingsMenu");
    }
}
