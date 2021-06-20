using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level3 : MonoBehaviour
{
    [SerializeField]
    private UIManager uiManager;
    [SerializeField]
    private ServiceLocator serviceLocator;
    private VoiceService voiceService;
    private AudioService audioService;
    private LevelService level;
    private LevelModule currentLevelModule;

    private bool timerIsRunning;
    private double timeRemaining;
    private double testTime;

    void Start()
    {
        voiceService = serviceLocator.GetVoiceService();
        audioService = serviceLocator.GetAudioService();
        level = serviceLocator.GetLevelService();

        currentLevelModule = level.GoToNextModule();

        List<AudioFragment> audioFragment = currentLevelModule.GetAudioFragments();

        List <AudioClip> audioClips = audioFragment.ConvertAll(clips => clips.GetAudioClip());

        
        Debug.Log(level.CurrentLevel);

        audioService.PlayAudio(audioClips, (currentIndex) => {
            uiManager.setLargeText(audioFragment[currentIndex].GetText());
        }, () => { 
            // When all done;
        });
    }

    void Update()
    {

    }

    private void PlayStep()
    {

    }
}
