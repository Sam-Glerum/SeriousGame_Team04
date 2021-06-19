using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class TextHandlerLevel1 : MonoBehaviour
{
    public float StartTimeRemaining = 10;
    public enum enTextLayout { ThreeQuestions, WrongAnswer, FullText };
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
    private AudioClip shakeSound, welcomeMessage, albertMessage, casetteMesage, level1completedMessage ;

    AudioSource audioSource;

    private string answer = ""; 
    private bool level2; 
    private enTextLayout currentEnTextLayout;
    private float timeRemaining = 10;
    private bool Step2Done = false; 

    protected bool timerIsRunning = false;


    // Start is called before the first frame update
    void Start()
    {
        timerIsRunning = true;
        SelectTextBox();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (answer.Equals( voiceController.VOICETEXT ) && CurrentStep == 2 && !Step2Done)
        {
            CheckStep2();
        }

        answer = voiceController.VOICETEXT;

        if (CurrentStep == 3)
        {
            CheckStep3();
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
        audioSource.Stop();
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
        timeRemaining = 20;
        audioSource.PlayOneShot(albertMessage);
        string text = "Ik loop hier al rond zolang ik leef, mijn vader was de oude directeur van deze plek.. dus je zou wel kunnen zeggen dat ik ben opgegroeid in dit theater. Ik ben hier altijd en ik ken alle hoeken en gaten van dit theater, maar toch kom je elke keer weer nieuwe dingen tegen.";
        largeText.text = text;
        voiceController.StartSpeaking(text);
        if (currentEnTextLayout != enTextLayout.FullText)
        {
            SwitchLayout(enTextLayout.FullText);
        }
    }

    private void Step0_1()
    {
        timeRemaining = 40;
        audioSource.PlayOneShot(albertMessage);

        string text = "Nu we het er toch over hebben, ik heb gisteren een cassettebandje gevonden. Ik ben hem helaas kwijt geraakt, maar ik denk dat ik al weet waar hij ligt. Zou je in de garderobe kunnen kijken of hij in mijn rode colbert met gouden knopen ligt?";
        largeText.text = text;
        voiceController.StartSpeaking(text);
        if (currentEnTextLayout != enTextLayout.FullText)
        {
            SwitchLayout(enTextLayout.FullText);
        }
    }

    private void Step1()
    {
        timeRemaining = 20;
        audioSource.PlayOneShot(casetteMesage);
        string text = "Nu we het er toch over hebben, ik heb gisteren een cassettebandje gevonden. Ik ben hem helaas kwijt geraakt, maar ik denk dat ik al weet waar hij ligt. Zou je in de garderobe kunnen kijken of hij in mijn rode colbert met gouden knopen ligt?";
        largeText.text = text;
        voiceController.StartSpeaking(text);

        if (currentEnTextLayout != enTextLayout.FullText)
        {
            SwitchLayout(enTextLayout.FullText);
        }
    }
    void CheckStep2()
    {

        if (answer.Equals("A", StringComparison.InvariantCultureIgnoreCase))
        {
            StepIncorrect(Qbutton1.text, 1);
            Step2Done = true;
        }
        if (answer.Equals("B", StringComparison.InvariantCultureIgnoreCase))
        {
            Step2_1();
            Step2Done = true;

        }
        if (answer.Equals("C", StringComparison.InvariantCultureIgnoreCase))
        {
            StepIncorrect(Qbutton2.text, 1);
            Step2Done = true;

        }
    }
    private void Step2()
    {
        audioSource.PlayOneShot(welcomeMessage);
        level2 = true;
        //Play voice recorded OR
        //Play voice Simulated
        largeText.text = "";
        float tijd =+ Time.deltaTime;

        string q1 = "Antwoord A  De casette zit in z'n voorvak!";
        string q2 = "Antwoord B  Nee de casette zit in z'n achterzak!";
        string q3 = "Antwoord C  De casette zit in zn binnenzak!";


        Qbutton0.text = q1;

        button0.onClick.AddListener(delegate () { StepIncorrect(Qbutton0.text, 1); });

        Qbutton1.text = q2;
        button1.onClick.AddListener(delegate () { Step2_1(); });

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

    private void Step2_1()
    {

        string text = ($"Antwoord B is juist, hij zit inderdaad in zn achterzak!");
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
        //Only say if you choose A,B or C
        answer.Remove(9);
        string text = ($"Niet correct het is niet {answer}!");
        largeText.text = text;
        voiceController.StartSpeaking(text);

        if (currentEnTextLayout != enTextLayout.WrongAnswer)
        {
            SwitchLayout(enTextLayout.WrongAnswer);
        }
        timeRemaining = 2;
        CurrentStep = originLevel-1;
    }

    private void CheckStep3()
    {

        if (phoneRotation.GyroX > 3)
        {
            SelectTextBox();
            audioSource.PlayOneShot(shakeSound);
        }
    }

    private void Step3()
    {
        audioSource.PlayOneShot(level1completedMessage);
        timerIsRunning = false;
        string text = "Gefeliciteerd je hebt level 1 gehaald.";
        largeText.text = text;
        voiceController.StartSpeaking(text);

        if (currentEnTextLayout != enTextLayout.FullText)
        {
            SwitchLayout(enTextLayout.FullText);
        }
    }

    private void Step4()
    {
        timerIsRunning = false;
        string text = "Het geheim ligt hem bij de ring….De ring staat in verbinding met de kurk De ring heeft er iets mee te maken verstoring in de zin waardoor je niet alles hoort, je mist hele belangrijke informatie";
        largeText.text = text;
        audioSource.PlayOneShot(welcomeMessage);

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
