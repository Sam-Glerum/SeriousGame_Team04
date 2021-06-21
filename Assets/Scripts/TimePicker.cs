using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TimePicker : MonoBehaviour
{
    public Button confirmButton;
    public TMP_InputField userTimeHourInput;
    public TMP_InputField userTimeMinuteInput;
    public TMP_Text errorText;

    private SceneLoader sceneLoader;
    private Player player = new Player();

    [SerializeField]
    private ServiceLocator serviceLocator;
    // Start is called before the first frame update
    void Start()
    {
        sceneLoader = new SceneLoader();
        confirmButton.onClick.AddListener(getTimeInputFromUser);
    }

    public void getTimeInputFromUser()
    {
        try
        {
            var timeString = userTimeHourInput.text.Trim() + ":" + userTimeMinuteInput.text.Trim();
            serviceLocator.GetLevelService().StartTime = DateTime.Parse(timeString);
            // Load next scene based on player's completed levels
            sceneLoader.LoadLevel(SceneManager.GetActiveScene().buildIndex + sceneLoader.LoadPlayerData(player).currentLevel);
        }
        catch (Exception e)
        {
            if (e is FormatException)
            {
                errorText.text = "De tijd wordt niet herkend. Probeer het opnieuw";
            }
        }
    }
}
