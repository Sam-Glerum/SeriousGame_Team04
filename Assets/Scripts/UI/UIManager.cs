using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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

    Transform questionObject;
    TMP_Text largeText;
    TMP_Text SelectedAnswer;

    private TextLayout currentTextLayout;

    private void Start()
    {
        largeText = GameObject.Find("LargeText").GetComponent<TMP_Text>();

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

    public void ShowQuestion(string question, List<string> answers)
    {
        largeText.text = "";

        questionObject = Instantiate(questionPrefab, new Vector2(250,112), Quaternion.identity);
        var questionText = questionObject.transform.GetChild(0).transform.GetChild(0).GetComponent<TMP_Text>();
        questionText.text = question; 

        foreach (var answer in answers)
        {
            Transform answerButton = Instantiate(buttonChildPrefab, transform.position, transform.rotation, questionObject);
            answerButton.GetComponent<Button>().onClick.AddListener(() => { SelectedAnswer = answerButton.Find("ButtonText").GetComponent<TMP_Text>(); level2.handleQuestionSelected(answer); });
            answerButton.Find("ButtonText").GetComponent<TMP_Text>().text = answer; 
        }
    }

    public void ShowAnswer(bool isRight)
    {
        if (!isRight) SelectedAnswer.color = Color.red;
        else
        {
            questionObject.transform.gameObject.SetActive(false);
            StartCoroutine(DestroyIn5Seconds());
            IEnumerator DestroyIn5Seconds()
            {
                yield return new WaitForSeconds(5);

                //After we have waited 5 seconds 
                //Destroy(questionObject);

            }
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

    public void SwitchLayout(TextLayout text)
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
                GetChildWithName(gameObject, "FullText").SetActive(true);
                currentTextLayout = TextLayout.RightAnswer;
                break;
            case TextLayout.WrongAnswer:
                GetChildWithName(gameObject, "ThreeQuestions").SetActive(false);
                GetChildWithName(gameObject, "FullText").SetActive(true);
                //timerIsRunning = true;
                //timeRemaining = 5;
                currentTextLayout = TextLayout.WrongAnswer;
                break;
            default:
                break;
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
