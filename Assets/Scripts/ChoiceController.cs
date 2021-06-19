using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoiceController : MonoBehaviour
{
    //Responsible for listening to voice activity
    [SerializeField]
    private VoiceController voiceController;

    AudioSource audioSource;

    [SerializeField]
    private AudioClip quizIntro;

    [SerializeField]
    private Button QuestionA;
    [SerializeField]
    private Button QuestionB;
    [SerializeField]
    private Button QuestionC;
    [SerializeField]
    private enum TextLayout { ThreeQuestions, WrongAnswer, RightAnswer };
    [SerializeField]
    private Answer rightAnswer;

    private TextLayout currentTextLayout;
    private enum Answer { A, B, C }

    private string answer = "";
    protected bool timerIsRunning = false;
    private float timeRemaining = 0;

    // Start is called before the first frame update
    void Start()
    {
        currentTextLayout = TextLayout.ThreeQuestions;
        StartQuiz();
    }

    // Update is called once per frame
    void Update()
    {
        //if(currentTextLayout.Equals(TextLayout.ThreeQuestions))
        //{
        //    answer = voiceController.VOICETEXT;
        //    if (answer.Equals(voiceController.VOICETEXT))
        //    {
        //        CheckAnswerUsingVoice();
        //    }
        //}

        if (currentTextLayout.Equals(TextLayout.WrongAnswer))
        {
            if (timerIsRunning)
            {
                if (timeRemaining > 0)
                {
                    timeRemaining -= Time.deltaTime;
                }
                else
                {
                    timerIsRunning = false;
                    SwitchLayout(TextLayout.ThreeQuestions);
                }
            }
        }
    }

    private void StartQuiz()
    {
        QuestionA.onClick.AddListener(delegate () { CheckAnswer(0); });
        QuestionB.onClick.AddListener(delegate () { CheckAnswer(1); });
        QuestionC.onClick.AddListener(delegate () { CheckAnswer(2); });
    }

    private void CheckAnswer(int answer)
    {
        if (answer == (int)rightAnswer)
        {
            SwitchLayout(TextLayout.RightAnswer);
        }
        else
        {
            SwitchLayout(TextLayout.WrongAnswer);
        }
    }

    private void CheckAnswerUsingVoice()
    {

            if (answer.Equals("A", StringComparison.InvariantCultureIgnoreCase))
            {
            CheckAnswer(0);
            }
            if (answer.Equals("B", StringComparison.InvariantCultureIgnoreCase))
            {
            CheckAnswer(1);
             }
            if (answer.Equals("C", StringComparison.InvariantCultureIgnoreCase))
            {
            CheckAnswer(2);
            }
    }


    private void SwitchLayout(TextLayout text)
    {
        switch (text)
        {
            case TextLayout.ThreeQuestions:
                GetChildWithName(gameObject, currentTextLayout.ToString()).SetActive(false);
                GetChildWithName(gameObject, "ThreeQuestions").SetActive(true);
                currentTextLayout = TextLayout.ThreeQuestions;
                break;
            case TextLayout.RightAnswer:
                GetChildWithName(gameObject, "ThreeQuestions").SetActive(false);
                GetChildWithName(gameObject, "RightAnswer").SetActive(true);
                currentTextLayout = TextLayout.RightAnswer;
                break;
            case TextLayout.WrongAnswer:
                GetChildWithName(gameObject, "ThreeQuestions").SetActive(false);
                GetChildWithName(gameObject, "WrongAnswer").SetActive(true);
                timerIsRunning = true;
                timeRemaining = 5;
                currentTextLayout = TextLayout.WrongAnswer;
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
