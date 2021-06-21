using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TextSpeech;
using UnityEngine.UI;
using UnityEngine.Android;
using TMPro;

[System.Serializable]
[CreateAssetMenu(fileName = "VoiceServiceConfig", menuName = "ScriptableObjects/VoiceService", order = 1)]
public class VoiceService : ScriptableObject
{
    const string LANG_CODE = "nl_NL";

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
    #region Speech to Text

    public void StartSpeaking(string message)
    {
        Debug.Log("Startspeak");
        TextToSpeech.instance.StartSpeak(message);
    }

    public void StopSpeaking()
    {
        Debug.Log("StopSpeak");
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
        VOICETEXT = result;
    }

    #endregion Text to Speech
    void Setup(string code)
    {
        TextToSpeech.instance.Setting(code, 1, 1);
        SpeechToText.instance.Setting(code);
    }

}
