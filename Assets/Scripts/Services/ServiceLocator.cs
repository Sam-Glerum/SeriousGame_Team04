using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "ServiceLocatorConfig", menuName = "ScriptableObjects/ServiceLocator", order = 1)]
public class ServiceLocator : ScriptableObject
{
    [SerializeField]
    GameObject audioServicePrefab;
    GameObject voiceControllerPrefab;
    AudioService audioService;
    VoiceController voiceController;


    public AudioService GetAudioService()
    {
        audioService = InstantiateIfNull<AudioService>(audioService, audioServicePrefab);

        return audioService;
    }

    public VoiceController GetVoiceController()
    {
        voiceController = InstantiateIfNull<VoiceController>(voiceController, voiceControllerPrefab);

        return voiceController;
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
