using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2 : MonoBehaviour
{
    [SerializeField]
    private UIManager uIManager;

    [SerializeField]
    private ServiceLocator serviceLocator;

    private AudioService audioService;
    private LevelService levelService;
    // Start is called before the first frame update
    void Start()
    {
        audioService = serviceLocator.GetAudioService();
        levelService = serviceLocator.GetLevelService();

        play();
    }

    private void play() {
        LevelModule currentLevelModule = levelService.GoToNextModule();

        List<AudioFragment> audioFragments = currentLevelModule.GetAudioFragments();

        List<AudioClip> audioClips = audioFragments.ConvertAll(audioFragments => audioFragments.GetAudioClip());

        audioService.PlayAudio(audioClips, (currentIndex) => {

            uIManager.setLargeText(audioFragments[currentIndex].GetText());
            uIManager.setImageTexture(audioFragments[currentIndex].GetTexture());

        }, () =>
        {
            if (currentLevelModule != null)
            {
                play();
            }
            else 
            {
                return;
            }
        });
    }
}
