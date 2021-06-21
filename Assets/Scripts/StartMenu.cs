using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static SaveSystem;
using static Player;

public class StartMenu : MonoBehaviour
{
    private SceneLoader sceneLoader;
    public Player player = new Player();

    private void Start()
    {
        sceneLoader = new SceneLoader();
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
        if (!sceneLoader.SaveFileExists(player))
        {
            // Creates a new save file if it does not yet exist
            SaveSystem.SaveData(player);
        }

        SceneManager.LoadScene("StartTimeInputScreen");
    }
}
