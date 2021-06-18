using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "ServiceLocatorConfig", menuName = "ScriptableObjects/ServiceLocator", order = 1)]
public class ServiceLocator : ScriptableObject
{
    [SerializeField]
    GameObject audioServicePrefab;
    AudioService audioService;

    [SerializeField]
    LevelService levelService;

    public AudioService GetAudioService()
    {
        audioService = InstantiateIfNull<AudioService>(audioService, audioServicePrefab);
        return audioService;
    }

    public LevelService GetLevelService()
    {
        return levelService;
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
