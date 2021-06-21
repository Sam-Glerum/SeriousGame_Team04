using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class UIManager : MonoBehaviour
{
    public enum TextLayout { ThreeQuestions, WrongAnswer, RightAnswer };

    [SerializeField]
    private Transform questionPrefab;
    [SerializeField]
    private TMP_Text questionChildPrefab;
    [SerializeField]
    private Transform buttonChildPrefab;
    [SerializeField]
    private Level2 level2;

    
    TMP_Text countDownTimer;
    Transform questionObject;
    TMP_Text largeText;
    TMP_Text SelectedAnswer;

    private TextLayout currentTextLayout;
    private float doubleClickTimer = 0;
    private void Start()
    {
        countDownTimer = GameObject.Find("CountdownTimer").GetComponent<TMP_Text>();
        largeText = GameObject.Find("LargeText").GetComponent<TMP_Text>();
    }
    void Update()
    {
        if (doubleClickTimer > 0)
        {
            doubleClickTimer -= Time.deltaTime;
        }
    }

    public void setLargeText(string text) {


        if (largeText == null)
        {
            Debug.Log("Error: LargeText instance is null!");
            return;
        }

        else {
            largeText.text = text;
        }

    }

    public void ShowCountDownTimer(Func<double> getRemainingTime)
    {
        double seconds = getRemainingTime(); //or whatever time you have
        string remainingtime = string.Format("{0:00}:{1:00}:{2:00}", seconds / 3600, (seconds / 60) % 60, seconds % 60);
        countDownTimer.text = remainingtime;
    }

    public void ShowQuestion(string question, List<string> answers)
    {
        largeText.text = "";

        questionObject = Instantiate(questionPrefab, new Vector2(250,112), Quaternion.identity);
        var questionText = questionObject.transform.GetChild(0).transform.GetChild(0).GetComponent<TMP_Text>();
        questionText.text = question; 

        foreach (var answer in answers)
        {
            Transform answerButton = Instantiate(buttonChildPrefab, transform.position, transform.rotation, questionObject);
            answerButton.Find("ButtonText").GetComponent<TMP_Text>().text = answer;

            answerButton.GetComponent<Button>().onClick.AddListener(() => { doubleClickTimer ++; });

            {
                answerButton.GetComponent<Button>().onClick.AddListener(() => { if (doubleClickTimer >= 1.1) { SelectedAnswer = answerButton.Find("ButtonText").GetComponent<TMP_Text>(); level2.handleQuestionSelected(answer); } });
            }
        }
    }

    public void ShowAnswer(bool isRight)
    {
        if (!isRight)
        {
            StartCoroutine(ChangeColorToColor(Color.red));
            IEnumerator ChangeColorToColor(Color color)
            {
                SelectedAnswer.color = color;
                yield return new WaitForSeconds(3);
                SelectedAnswer.color = Color.black;
            }
        }

        else
        {
            questionObject.transform.gameObject.SetActive(false);
            StartCoroutine(DestroyIn5Seconds());
            IEnumerator DestroyIn5Seconds()
            {
                yield return new WaitForSeconds(10);

                //After we have waited 5 seconds 
                Destroy(questionObject.gameObject);

            }
        }
    }

    public void setImageTexture(Texture texture)
    {
        RawImage largeText = GameObject.Find("Albert").GetComponent<RawImage>(); ;

        if (largeText == null)
        {
            Debug.Log("Error: Albert instance is null!");
            return;
        }
        else
        {
            largeText.texture = texture;
        }
    }
}
