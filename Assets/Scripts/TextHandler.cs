using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class TextHandler : MonoBehaviour
{
    public float StartTimeRemaining = 10;
    public enum enTextLayout { FullText, ThreeQuestions, WrongAnswer };
    public int CurrentStep = -1;

    [SerializeField]
    private TMP_Text largeText, timeText;
    [SerializeField]
    private TMP_Text Qbutton0, Qbutton1, Qbutton2;
    [SerializeField]
    private Button button0, button1, button2;
    [SerializeField]
    private VoiceController voiceController;
    [SerializeField]
    private PhoneRotation phoneRotation;
    [SerializeField]
    private AudioClip shakeSound, secretMessage, Level3_1, Level3_2, Level3_3, Level3_4;
    [SerializeField]
    private Texture Albert, Cassette;
    [SerializeField]
    private GameObject Image;
    AudioSource audioSource;
    RawImage rawImage;

    private string answer = ""; 
    private enTextLayout currentEnTextLayout;
    private float timeRemaining = 10;
    private bool Step2Done = false;
    private bool Step6Done = false;

    protected bool timerIsRunning = false;


    // Start is called before the first frame update
    void Start()
    {
        rawImage = Image.transform.GetComponent<RawImage>();

        timerIsRunning = true;
        SelectTextBox();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (CurrentStep == 2 && !Step2Done)
        {
            CheckStep2();
        }

        answer = voiceController.VOICETEXT;

        if (CurrentStep == 6 && !Step6Done)
        {
            CheckStep6();
        }


        if (CurrentStep == 7)
        {
            CheckStep7();
        }

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
        StopAllCoroutines();
        answer = "";
        CurrentStep = step;

        string function = $"Step{step}";
        Invoke(function, 0);
        timeRemaining = StartTimeRemaining;
        timerIsRunning = true;
    }

    public void SelectTextBox()
    {
        StopAllCoroutines();
        answer = "";
        CurrentStep++;

        string function = $"Step{CurrentStep}";
        Invoke(function, 0);
        timeRemaining = StartTimeRemaining;
        timerIsRunning = true;
    }

    private void Step0()
    {
        audioSource.PlayOneShot(Level3_2);

        timeRemaining = 17;
        string text = "In mijn truc van de drijvende kurk is de kurk natuurlijk erg belangrijk. Ooit gaf ik een waardevol stuk aan de oude directeur van het theater. Hij had hem op een plek gezet waar water veel wordt doorgespoeld. Misschien moet je daar is gaan zoeken...";
        largeText.text = text;

        if (currentEnTextLayout != enTextLayout.FullText)
        {
            SwitchLayout(enTextLayout.FullText);
        }
    }


    private void Step1()
    {
        audioSource.PlayOneShot(Level3_3);

        timeRemaining = 10;
        string text = "Albert... Oh Jaa ";
        largeText.text = text;

        if (currentEnTextLayout != enTextLayout.FullText)
        {
            SwitchLayout(enTextLayout.FullText);
        }
    }

    void CheckStep2()
    {

        if (answer.Equals("Glas", StringComparison.InvariantCultureIgnoreCase))
        {
            StepIncorrect(Qbutton1.text, 1);
            Step2Done = true;
        }
        if (answer.Equals("Hout", StringComparison.InvariantCultureIgnoreCase))
        {
            Step2_1();
            Step2Done = true;

        }
        if (answer.Equals("Steen", StringComparison.InvariantCultureIgnoreCase))
        {
            StepIncorrect(Qbutton2.text, 1);
            Step2Done = true;

        }
    }
    private void Step2()
    {
        audioSource.PlayOneShot(Level3_4);
        largeText.text = "";
        timerIsRunning = false;

        string q1 = "Antwoord A  Glas";
        string q2 = "Antwoord B  Hout";
        string q3 = "Antwoord C  Steen";

        Qbutton0.text = q1;

        button0.onClick.AddListener(delegate () { StepIncorrect(Qbutton0.text, 1); });

        Qbutton1.text = q2;
        button1.onClick.AddListener(delegate () { Step2_1(); });

        Qbutton2.text = q3;
        button2.onClick.AddListener(delegate () { StepIncorrect(Qbutton2.text, 1); });

        if (currentEnTextLayout != enTextLayout.ThreeQuestions)
        {
            SwitchLayout(enTextLayout.ThreeQuestions);
        }
    }

    private void Step2_1()
    {
        audioSource.Stop();
        string text = ($"Dat is correct het is natuurlijk van hout.");
        largeText.text = text;
        voiceController.StartSpeaking(text);

        if (currentEnTextLayout != enTextLayout.FullText)
        {
            SwitchLayout(enTextLayout.FullText);
        }
        timeRemaining = 5;
        timerIsRunning = true;

    }


    private void Step3()
    {
        timeRemaining = 5;

        timerIsRunning = true;
        string text = "Toen was het bandje afgelopen.";
        largeText.text = text;
        voiceController.StartSpeaking(text);
        if (currentEnTextLayout != enTextLayout.FullText)
        {
            SwitchLayout(enTextLayout.FullText);
        }
    }

    private void Step4()
    {
        string text = "Waar is nu de overige informatie van Albert? ";
        largeText.text = text;
        voiceController.StartSpeaking(text);
        if (currentEnTextLayout != enTextLayout.FullText)
        {
            SwitchLayout(enTextLayout.FullText);
        }
    }

    private void Step5()
    {
        timeRemaining = 10;
        string text = "Er komen zo 3 opties. A, B en C. Klik op het scherm en zeg luidop A, B of C om antwoord te geven.";
        largeText.text = text;
        voiceController.StartSpeaking(text);

        if (currentEnTextLayout != enTextLayout.FullText)
        {
            SwitchLayout(enTextLayout.FullText);
        }
    }
    void CheckStep6()
    {

        if (answer.Equals("A", StringComparison.InvariantCultureIgnoreCase))
        {
            StepIncorrect(Qbutton1.text, 1);
            Step6Done = true;
        }
        if (answer.Equals("B", StringComparison.InvariantCultureIgnoreCase))
        {
            Step6_1();
            Step6Done = true;
        }
        if (answer.Equals("C", StringComparison.InvariantCultureIgnoreCase))
        {
            StepIncorrect(Qbutton2.text, 1);
            Step6Done = true;
        }
    }
    private void Step6()
    {
        //Play voice recorded OR
        //Play voice Simulated
        largeText.text = "";
        float tijd =+ Time.deltaTime;

        string q1 = "Antwoord A  Het bandje terugdraaien, misschien hebben we wat gemist!";
        string q2 = "Antwoord B  De B zijde van het bandje beluisteren.";
        string q3 = "Antwoord C  Misschien is er in zijn colbert nog iets te vinden.";


        Qbutton0.text = q1;

        button0.onClick.AddListener(delegate () { StepIncorrect(Qbutton0.text, 1); });

        Qbutton1.text = q2;
        button1.onClick.AddListener(delegate () { Step6_1(); });

        Qbutton2.text = q3;
        button2.onClick.AddListener(delegate () { StepIncorrect(Qbutton2.text, 1); });



        StartCoroutine(CoRoutineQ1());

        

        if (currentEnTextLayout != enTextLayout.ThreeQuestions)
        {
            SwitchLayout(enTextLayout.ThreeQuestions);
        }

        IEnumerator CoRoutineQ1()
        {
            voiceController.StartSpeaking(q1);
            yield return new WaitForSeconds(5);
            StartCoroutine(CoRoutineQ2());

        }
        IEnumerator CoRoutineQ2()
        {
            voiceController.StartSpeaking(q2);
            yield return new WaitForSeconds(5);
            StartCoroutine(CoRoutineQ3());

        }
        IEnumerator CoRoutineQ3()
        {
            voiceController.StartSpeaking(q3);
            yield return new WaitForSeconds(5);
        }
    }

    private void Step6_1()
    {
        StopAllCoroutines();
        string text = ($"Laten we proberen om de B zijde te beluisteren!");
        largeText.text = text;
        voiceController.StartSpeaking(text);

        if (currentEnTextLayout != enTextLayout.FullText)
        {
            SwitchLayout(enTextLayout.FullText);
        }
        timeRemaining = 3;
        timerIsRunning = true;

    }



    private void StepIncorrect(string answer, int originLevel)
    {
        Step2Done = false;
        Step6Done = false;

        //Only say if you choose A,B or C
        audioSource.Stop();

        answer.Remove(9);
        string text = ($"Niet correct het is niet {answer}!");
        largeText.text = text;
        voiceController.StartSpeaking(text);

        if (currentEnTextLayout != enTextLayout.WrongAnswer)
        {
            SwitchLayout(enTextLayout.WrongAnswer);
        }
        timeRemaining = 5;
        CurrentStep = originLevel-1;
    }

    private void CheckStep7()
    {

        if (phoneRotation.GyroX > 6 )
        {
            SelectTextBox();
            audioSource.PlayOneShot(shakeSound);
        }
    }

    private void Step7()
    {
        rawImage.texture = Cassette; 
        timerIsRunning = false;
        string text = "Schud het scherm! om de cassette te verwisselen";
        largeText.text = text;
        voiceController.StartSpeaking(text);


        if (currentEnTextLayout != enTextLayout.FullText)
        {
            SwitchLayout(enTextLayout.FullText);
        }
    }

    private void Step8()
    {
        rawImage.texture = Albert;

        timerIsRunning = false;
        string text = "Het geheim ligt hem bij de ring...";
        largeText.text = text;
        audioSource.PlayOneShot(secretMessage);

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
                timerIsRunning = true;
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
                timerIsRunning = true;
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
