using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class SwipeOverDescriptionReader : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    string description;

    [SerializeField]
    ServiceLocator serviceLocator;
    VoiceService voiceService;

    bool isReading;

    private void Start()
    {
        voiceService = serviceLocator.GetVoiceService();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (description != null)
        {
            Handheld.Vibrate();
            voiceService.StopSpeaking();
            voiceService.StartSpeaking(description);
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
