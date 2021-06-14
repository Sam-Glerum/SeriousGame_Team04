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
        PlayerData playerData = SaveSystem.LoadData(player);

        // Checks if a save file exists
        if (SaveSystem.LoadData(player) != null)
        {

            if (playerData.currentLevel > 0)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + playerData.currentLevel);
            }    
          
}
        else
        {
            player.currentLevel = 0;
            // Creates a new save file if it does not yet exist
            SaveSystem.SaveData(player);
            //Loads the first level in the build settings list(after the menu scene)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void LoadScene1()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void OpenSettings()
    {
        SceneManager.LoadScene("SettingsMenu");
    }
}
