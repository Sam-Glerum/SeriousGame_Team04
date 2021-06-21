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

    private void Start()
    {
        voiceService = serviceLocator.GetVoiceService();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        var textObject = this.gameObject.GetComponent<TMP_Text>();

        if (textObject != null)
        {
            string readableText = textObject.text;
            voiceService.StopSpeaking();
            voiceService.StartSpeaking(readableText);
        }
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        voiceService.StopSpeaking();
    }
}
