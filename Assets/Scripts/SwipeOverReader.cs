using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class SwipeOverReader : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    ServiceLocator serviceLocator;
    VoiceService voiceService;
    bool isReading;

    [SerializeField]
    string addidtionalText;

    private void Start()
    {
        voiceService = serviceLocator.GetVoiceService();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        var textObject = this.gameObject.GetComponent<TMP_Text>();

        if (textObject != null)
        {
            string readableText = textObject.text + " " + (addidtionalText ?? "");
            Handheld.Vibrate();
            voiceService.StopSpeaking();
            voiceService.StartSpeaking(readableText);
            isReading = true;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (isReading)
        {
            voiceService.StopSpeaking();
            isReading = false;
        }
    }
}
