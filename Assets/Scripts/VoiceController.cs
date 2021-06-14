using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TextSpeech;
using UnityEngine.UI;
using UnityEngine.Android;
using TMPro;
public class VoiceController : MonoBehaviour
{
    const string LANG_CODE = "nl_NL";

    [SerializeField]
    TMP_Text uiText;

    public string VOICETEXT;

    private void Start()
    {
        Setup(LANG_CODE);

        SpeechToText.instance.onResultCallback = OnFinalSpeechResult;
        TextToSpeech.instance.onStartCallBack = OnSpeakStart;
        TextToSpeech.instance.onDoneCallback = OnSpeakStop;

        CheckPermissions();
    }
    void CheckPermissions()
    {
#if UNITY_ANDROID
        
        if (!Permission.HasUserAuthorizedPermission(Permission.Microphone))
            Permission.RequestUserPermission(Permission.Microphone);
        
#endif
    }
    #region Text to Speech

    public void StartSpeaking(string message)
    {
        TextToSpeech.instance.StartSpeak(message);
    }

    public void StopSpeaking()
    {
        TextToSpeech.instance.StopSpeak();
    }

    public void OnSpeakStart()
    {
        Debug.Log("Speaking...");
    }

    public void OnSpeakStop()
    {
        Debug.Log("Stopped Speaking");
    }

    #endregion Text to Speech

    #region Text to Speech

    public void StartListening()
    {
        SpeechToText.instance.StartRecording();
    }

    public void StopListening()
    {
        SpeechToText.instance.StopRecording();
    }

    void OnFinalSpeechResult(string result)
    {
        uiText.text = result;
        VOICETEXT = result;
    }

    #endregion Text to Speech
    void Setup(string code)
    {
        TextToSpeech.instance.Setting(code, 1, 1);
        SpeechToText.instance.Setting(code);
    }

    public void test()
    {
        Debug.Log("Test!");
    }
}
