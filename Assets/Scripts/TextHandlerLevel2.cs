//using System;
//using System.Collections;
//using TMPro;
//using UnityEngine;
//using UnityEngine.UI;

//[RequireComponent(typeof(AudioSource))]
//public class TextHandlerLevel2 : MonoBehaviour
//{

//    [SerializeField]
//    private ServiceLocator serviceLocator;



//    private UIManager uIManager;

//    public float StartTimeRemaining = 10;
//    public enum enTextLayout { FullText, ThreeQuestions, WrongAnswer };
//    public int CurrentStep = -1;

//    [SerializeField]
//    private TMP_Text largeText, timeText;
//    [SerializeField]
//    private TMP_Text Qbutton0, Qbutton1, Qbutton2;
//    [SerializeField]
//    private Button button0, button1, button2;
//    [SerializeField]
//    private PhoneRotation phoneRotation;
//    [SerializeField]
//    private AudioClip shakeSound, secretMessage;

//    [SerializeField]
//    private AudioClip audio_1;

//    [SerializeField]
//    private AudioClip audio_2;

//    [SerializeField]
//    private AudioClip audio_3;

//    [SerializeField]
//    private AudioClip audio_4;

//    AudioSource audioSource;

//    [SerializeField]
//    private RawImage img;

//    public Texture image_lock; //Reference to a preset image
//    public Texture image_unlock; //Reference to a preset image
//    public Texture image_albert; //Reference to a preset image

//    private string answer = ""; 
//    private bool level2; 
//    private enTextLayout currentEnTextLayout;
//    private float timeRemaining = 10;
//    private bool Step2Done = false; 

//    protected bool timerIsRunning = false;


//    // Start is called before the first frame update
//    void Start()
//    {
//        var test = this.serviceLocator.GetLevelService().GetCurrentModule();
//        Debug.Log(test);


//        uIManager = gameObject.AddComponent<UIManager>();


//        timerIsRunning = true;
//        SelectTextBox();
//        audioSource = GetComponent<AudioSource>();
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        if (answer.Equals( voiceController.VOICETEXT ) && CurrentStep == 2 && !Step2Done)
//        {
//            CheckStep2();
//        }

//        answer = voiceController.VOICETEXT;

//        if (CurrentStep == 3)
//        {
//            CheckStep3();
//        }

//        if (timerIsRunning)
//        {
//            if (timeRemaining > 0)
//            {
//                timeRemaining -= Time.deltaTime;
//                DisplayTime(timeRemaining);
//            }
//            else
//            {
//                Debug.Log("Time has run out, next slide");
//                timeRemaining = 0;
//                timerIsRunning = false;

//                SelectTextBox();
//            }
//        }
//    }
//    public void DisplayTime(float timeToDisplay)
//    {
//        timeToDisplay += 1;

//        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
//        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

//        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
//    }
//    public void SelectTextBox(int step)
//    {
//        StopAllCoroutines();
//        answer = "";
//        CurrentStep = step;

//        string function = $"Step{step}";
//        Invoke(function, 0);
//        timeRemaining = StartTimeRemaining;
//        timerIsRunning = true;
//    }

//    public void SelectTextBox()
//    {
//        StopAllCoroutines();
//        answer = "";
//        CurrentStep++;

//        string function = $"Step{CurrentStep}";
//        Invoke(function, 0);
//        timeRemaining = StartTimeRemaining;
//        timerIsRunning = true;
//    }

//    private void Step0()
//    {
//        timeRemaining = 20;

//        audioSource.PlayOneShot(audio_1);


//        string text = "Welkom terug in het theater, leuk dat je er weer bent!";
//        largeText.text = text;

//    }

//    private void Step0_1()
//    {

//        string text = "Waar is nu de overige informatie van Albert? ";
//        largeText.text = text;
//        voiceController.StartSpeaking(text);
//        if (currentEnTextLayout != enTextLayout.FullText)
//        {
//            SwitchLayout(enTextLayout.FullText);
//        }
//    }

//    private void Step1()
//    {

//        audioSource.PlayOneShot(audio_2);
//        img.texture = image_lock;

//        timeRemaining = 7;


//        string text = "Wat is de geheime code, 200, 10 of 850?"; 
//        largeText.text = text;
//        voiceController.StartSpeaking(text);

//        if (currentEnTextLayout != enTextLayout.FullText)
//        {
//            SwitchLayout(enTextLayout.FullText);
//        }
//    }
//    void CheckStep2()
//    {

//        if (answer.Equals("A", StringComparison.InvariantCultureIgnoreCase))
//        {
//            StepIncorrect(Qbutton1.text, 1);
//            Step2Done = true;
//        }
//        if (answer.Equals("B", StringComparison.InvariantCultureIgnoreCase))
//        {
//            Step2_1();
//            Step2Done = true;

//        }
//        if (answer.Equals("C", StringComparison.InvariantCultureIgnoreCase))
//        {
//            StepIncorrect(Qbutton2.text, 1);
//            Step2Done = true;

//        }
//    }
//    private void Step2()
//    {
//        level2 = true;
//        //Play voice recorded OR
//        //Play voice Simulated
//        largeText.text = "";
//        float tijd =+ Time.deltaTime;

//        string q1 = "Antwoord A  200";
//        string q2 = "Antwoord B  10";
//        string q3 = "Antwoord C  850";


//        Qbutton0.text = q1;

//        button0.onClick.AddListener(delegate () { StepIncorrect(Qbutton0.text, 1); });

//        Qbutton1.text = q2;
//        button1.onClick.AddListener(delegate () { Step2_1(); });

//        Qbutton2.text = q3;
//        button2.onClick.AddListener(delegate () { StepIncorrect(Qbutton2.text, 1); });



//        StartCoroutine(CoRoutineQ1());

        

//        if (currentEnTextLayout != enTextLayout.ThreeQuestions)
//        {
//            SwitchLayout(enTextLayout.ThreeQuestions);
//        }

//        IEnumerator CoRoutineQ1()
//        {
//            voiceController.StartSpeaking(q1);
//            yield return new WaitForSeconds(5);
//            StartCoroutine(CoRoutineQ2());

//        }
//        IEnumerator CoRoutineQ2()
//        {
//            voiceController.StartSpeaking(q2);
//            yield return new WaitForSeconds(5);
//            StartCoroutine(CoRoutineQ3());

//        }
//        IEnumerator CoRoutineQ3()
//        {
//            voiceController.StartSpeaking(q3);
//            yield return new WaitForSeconds(5);
//        }
//    }

//    private void Step2_1()
//    {
//        audioSource.PlayOneShot(audio_3);

//        img.texture = image_unlock;

//        string text = ($"Je hebt de code gekraakt!");
//        largeText.text = text;
//        voiceController.StartSpeaking(text);

//        if (currentEnTextLayout != enTextLayout.FullText)
//        {
//            SwitchLayout(enTextLayout.FullText);
//        }
//        timeRemaining = 3;
//        timerIsRunning = true;

//    }



//    private void StepIncorrect(string answer, int originLevel)
//    {
//        //Only say if you choose A,B or C
//        answer.Remove(9);
//        string text = ($"De code is niet gekraakt, {answer} is niet het juiste antwoord. Probeer het opnieuw!");
//        largeText.text = text;
//        voiceController.StartSpeaking(text);

//        if (currentEnTextLayout != enTextLayout.WrongAnswer)
//        {
//            SwitchLayout(enTextLayout.WrongAnswer);
//        }
//        timeRemaining = 5;
//        CurrentStep = originLevel-1;
//    }

//    private void CheckStep3()
//    {
//        bool move = false;

//        if (phoneRotation.GyroX > 3)
//        {
//            SelectTextBox();
//            audioSource.PlayOneShot(shakeSound);
//        }
//    }

//    private void Step3()
//    {
//        timerIsRunning = false;

//        img.texture = image_albert;

//        audioSource.PlayOneShot(audio_4);

//        string text = "Leid de ogen van de toeschouwer naar het kistje en leid jouw aandacht naar je stoel.";
//        largeText.text = text;
//        voiceController.StartSpeaking(text);

//        if (currentEnTextLayout != enTextLayout.FullText)
//        {
//            SwitchLayout(enTextLayout.FullText);
//        }
//    }

//    private void Step4()
//    {
//        timerIsRunning = false;
//        string text = "Het geheim ligt hem bij de ring….De ring staat in verbinding met de kurk De ring heeft er iets mee te maken verstoring in de zin waardoor je niet alles hoort, je mist hele belangrijke informatie";
//        largeText.text = text;
//        audioSource.PlayOneShot(secretMessage);

//        if (currentEnTextLayout != enTextLayout.FullText)
//        {
//            SwitchLayout(enTextLayout.FullText);
//        }
//    }

//    private void SwitchLayout(enTextLayout text)
//    {
//        switch (text)
//        {
//            case enTextLayout.FullText:
//                GetChildWithName(gameObject, currentEnTextLayout.ToString()).SetActive(false);
//                GetChildWithName(gameObject, "FullText").SetActive(true);
//                currentEnTextLayout = enTextLayout.FullText;
//                timerIsRunning = true;
//                break;
//            case enTextLayout.ThreeQuestions:
//                GetChildWithName(gameObject, currentEnTextLayout.ToString()).SetActive(false);
//                GetChildWithName(gameObject, "ContinueButton").SetActive(false);
//                GetChildWithName(gameObject, "ThreeQuestions").SetActive(true);
//                currentEnTextLayout = enTextLayout.ThreeQuestions;
//                timerIsRunning = false;
//                break;
//            case enTextLayout.WrongAnswer:
//                GetChildWithName(gameObject, currentEnTextLayout.ToString()).SetActive(false);
//                GetChildWithName(gameObject, "ContinueButton").SetActive(false);
//                GetChildWithName(gameObject, "FullText").SetActive(true);
//                currentEnTextLayout = enTextLayout.FullText;
//                timerIsRunning = true;
//                break;
//            default:
//                break;
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
