//using System;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;

//public class ChoiceController : MonoBehaviour
//{
//    //Responsible for listening to voice activity
//    private VoiceService voiceService;
//    [SerializeField]
//    private ServiceLocator serviceLocator;
//    [SerializeField]
//    private AudioClip quizIntro;

//    [SerializeField]
//    private Button QuestionA;
//    [SerializeField]
//    private Button QuestionB;
//    [SerializeField]
//    private Button QuestionC;
//    [SerializeField]
//    public enum TextLayout { ThreeQuestions, WrongAnswer, RightAnswer };
//    [SerializeField]
//    private Answer rightAnswer;

//    private TextLayout currentTextLayout;

//    private UIManager uIManager;

//    private enum Answer { A, B, C }

//    private string answer = "";
//    protected bool timerIsRunning = false;
//    private float timeRemaining = 0;

//    void Start()
//    {
//        voiceService = serviceLocator.GetVoiceService();
//        currentTextLayout = TextLayout.ThreeQuestions;
//        InitializeListeners();
//    }

//    void Update()
//    {
//        if (currentTextLayout.Equals(TextLayout.ThreeQuestions))
//        {
//            answer = voiceService.VOICETEXT;
//            if (answer.Equals(voiceService.VOICETEXT))
//            {
//                CheckAnswerUsingVoice();
//            }
//        }

//        if (currentTextLayout.Equals(TextLayout.WrongAnswer))
//        {
//            if (timerIsRunning)
//            {
//                if (timeRemaining > 0)
//                {
//                    timeRemaining -= Time.deltaTime;
//                }
//                else
//                {
//                    timerIsRunning = false;
//                    SwitchLayout(TextLayout.ThreeQuestions);
//                }
//            }
//        }
//    }

//    private void InitializeListeners()
//    {
//        QuestionA.onClick.AddListener(delegate () { CheckAnswer(0); });
//        QuestionB.onClick.AddListener(delegate () { CheckAnswer(1); });
//        QuestionC.onClick.AddListener(delegate () { CheckAnswer(2); });
//    }



//    private void CheckAnswerUsingVoice()
//    {

//        if (answer.Equals("A", StringComparison.InvariantCultureIgnoreCase))
//        {
//            CheckAnswer(0);
//        }
//        if (answer.Equals("B", StringComparison.InvariantCultureIgnoreCase))
//        {
//            CheckAnswer(1);
//        }
//        if (answer.Equals("C", StringComparison.InvariantCultureIgnoreCase))
//        {
//            CheckAnswer(2);
//        }
//    }

//    private GameObject GetChildWithName(GameObject obj, string name)
//    {
//        Transform trans = obj.transform;
//        Transform childTrans = trans.Find(name);
//        if (childTrans != null)
//        {
//            return childTrans.gameObject;
//        }
//        else
//        {
//            return null;
//        }
//    }
//}