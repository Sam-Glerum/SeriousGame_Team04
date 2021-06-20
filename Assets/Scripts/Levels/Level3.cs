using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level3 : MonoBehaviour
{
    [SerializeField]
    private ServiceLocator serviceLocator;
    private VoiceService voiceService;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        LevelService level = serviceLocator.GetLevelService();
        int currentLevel = serviceLocator.GetLevelService().CurrentLevel;
        double time = serviceLocator.GetLevelService().GetAvaiableTimeInSeconds();

        Debug.Log(currentLevel);
        Debug.Log(time);

        Debug.Log(level.currentLevelModule.GetDuration());

        level.GoToNextModule();

        foreach (var fragments in level.currentLevelModule.GetAudioFragments())
        {
            Debug.Log("Play fragment");
            audioSource.PlayOneShot(fragments.audioSource);

        }
        //AudioFragment audioFragment = level.currentLevelModule.GetAudioFragments()[0];
        //audioSource.PlayOneShot(audioFragment.audioSource);

    }

    void Update()
    {
        
    }
}
