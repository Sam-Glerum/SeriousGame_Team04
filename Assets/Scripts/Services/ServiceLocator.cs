using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "ServiceLocatorConfig", menuName = "ScriptableObjects/ServiceLocator", order = 1)]
public class ServiceLocator : ScriptableObject
{
    [SerializeField]
    GameObject audioServicePrefab, voiceServicePrefab;
    [SerializeField]
    LevelService levelService;
    [SerializeField]
    VoiceService voiceService;

    AudioService audioService;

    public AudioService GetAudioService()
    {
        audioService = InstantiateIfNull<AudioService>(audioService, audioServicePrefab);
        return audioService;
    }

    public LevelService GetLevelService()
    {
        return levelService;
    }

    public VoiceService GetVoiceService()
    {
        return voiceService;
    }

    private T InstantiateIfNull<T>(T component, GameObject prefab)
    {
        if (component == null)
        {
            GameObject gameObject = Instantiate(prefab);
            component = gameObject.GetComponent<T>();
            if (component is UnityEngine.Object) DontDestroyOnLoad(component as UnityEngine.Object);
        }

        return component;
    }
}
