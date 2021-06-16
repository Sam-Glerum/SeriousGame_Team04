using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "ServiceLocatorConfig", menuName = "ScriptableObjects/ServiceLocator", order = 1)]
public class ServiceLocator : ScriptableObject
{
    [SerializeField]
    AudioService audioService;

    public AudioService GetAudioService()
    {
        return audioService;
    }
}
