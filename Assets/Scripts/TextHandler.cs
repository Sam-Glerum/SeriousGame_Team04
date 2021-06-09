using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TextHandler : MonoBehaviour
{
    public float StartTimeRemaining = 10;
    public enum enTextLayout { FullText, ThreeQuestions, WrongAnswer };
    public int CurrentStep = 0;

    [SerializeField]
    private TMP_Text largeText, questionText, timeText;
    [SerializeField]
    private TMP_Text Qbutton0, Qbutton1, Qbutton2;
    [SerializeField]
    private Button button0, button1, button2;

    protected bool timerIsRunning = false;

    private enTextLayout currentEnTextLayout;
    private float timeRemaining = 10;

    // Start is called before the first frame update
    void Start()
    {
        timerIsRunning = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else
            {
                Debug.Log("Time has run out, next slide");
                timeRemaining = 0;
                timerIsRunning = false;

                SelectTextBox();
            }
        }
    }
    public void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
    public void SelectTextBox(int step)
    {
        CurrentStep = step;

        string function = $"Step{step}";
        Invoke(function, 0);
        timeRemaining = StartTimeRemaining;
        timerIsRunning = true;
    }

    public void SelectTextBox()
    {
        CurrentStep++;

        string function = $"Step{CurrentStep}";
        Invoke(function, 0);
        timeRemaining = StartTimeRemaining;
        timerIsRunning = true;
    }

    private void Step0()
    {
        largeText.text = "Step0 Text etcetra";

        if (currentEnTextLayout != enTextLayout.FullText)
        {
            SwitchLayout(enTextLayout.FullText);
        }
    }

    private void Step1()
    {
        //Play voice recorded OR
        //Play voice Simulated

        questionText.text = "What colour?";
        Qbutton0.text = "Red";
        button0.onClick.AddListener(delegate () { Step1_1(); });
        Qbutton1.text = "Green";
        button1.onClick.AddListener(delegate () { StepIncorrect(Qbutton1.text, 1); });
        Qbutton2.text = "Blue";
        button2.onClick.AddListener(delegate () { StepIncorrect(Qbutton2.text, 1); });

        if (currentEnTextLayout != enTextLayout.ThreeQuestions)
        {
            SwitchLayout(enTextLayout.ThreeQuestions);
        }
    }

    private void Step1_1 ()
    {
        largeText.text = "Correct!";

        if (currentEnTextLayout != enTextLayout.FullText)
        {
            SwitchLayout(enTextLayout.FullText);
        }
        timeRemaining = 2;
    }

    private void StepIncorrect(string answer, int originLevel)
    {
        largeText.text = $"Incorrect it is not {answer}!";

        if (currentEnTextLayout != enTextLayout.WrongAnswer)
        {
            SwitchLayout(enTextLayout.WrongAnswer);
        }
        timeRemaining = 2;
        CurrentStep = originLevel-1;
    }


    private void Step2()
    {
        largeText.text = "Step2 Text etcetra";

        if (currentEnTextLayout != enTextLayout.FullText)
        {
            SwitchLayout(enTextLayout.FullText);
        }
    }

    private void SwitchLayout(enTextLayout text)
    {
        switch (text)
        {
            case enTextLayout.FullText:
                GetChildWithName(gameObject, currentEnTextLayout.ToString()).SetActive(false);
                GetChildWithName(gameObject, "FullText").SetActive(true);
                currentEnTextLayout = enTextLayout.FullText;
                break;
            case enTextLayout.ThreeQuestions:
                GetChildWithName(gameObject, currentEnTextLayout.ToString()).SetActive(false);
                GetChildWithName(gameObject, "ContinueButton").SetActive(false);
                GetChildWithName(gameObject, "ThreeQuestions").SetActive(true);
                currentEnTextLayout = enTextLayout.ThreeQuestions;
                timerIsRunning = false;
                break;
            case enTextLayout.WrongAnswer:
                GetChildWithName(gameObject, currentEnTextLayout.ToString()).SetActive(false);
                GetChildWithName(gameObject, "ContinueButton").SetActive(false);
                GetChildWithName(gameObject, "FullText").SetActive(true);
                currentEnTextLayout = enTextLayout.FullText;
                break;
            default:
                break;
        }
    }
    private GameObject GetChildWithName(GameObject obj, string name)
    {
        Transform trans = obj.transform;
        Transform childTrans = trans.Find(name);
        if (childTrans != null)
        {
            return childTrans.gameObject;
        }
        else
        {
            return null;
        }
    }
}
