using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimePicker : MonoBehaviour
{
    public Button confirmButton;
    public TMP_InputField userTimeHourInput;
    public TMP_InputField userTimeMinuteInput;
    public TMP_Text errorText;

    [SerializeField]
    private ServiceLocator serviceLocator;
    // Start is called before the first frame update
    void Start()
    {
        confirmButton.onClick.AddListener(getTimeInputFromUser);
    }

    public void getTimeInputFromUser()
    {
        try
        {
            var timeString = userTimeHourInput.text.Trim() + ":" + userTimeMinuteInput.text.Trim();
            serviceLocator.GetLevelService().StartTime = DateTime.Parse(timeString);
        }
        catch (Exception e)
        {
            Debug.Log(e);
            if (e is FormatException)
            {
                errorText.text = "De tijd wordt niet herkend. Probeer het opnieuw";
                Debug.Log("fout!");
            }
        }
    }
}
