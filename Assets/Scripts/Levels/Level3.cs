//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class Level3 : MonoBehaviour
//{
//    [SerializeField]
//    private UIManager uIManager;

//    [SerializeField]
//    private ServiceLocator serviceLocator;
//    [SerializeField]
//    //private ChoiceController choiceController;

//    private AudioService audioService;
//    private LevelService levelService;
//    // Start is called before the first frame update
//    void Start()
//    {
//        audioService = serviceLocator.GetAudioService();
//        levelService = serviceLocator.GetLevelService();


//        play();
//    }

//    private void play()
//    {
//        LevelModule currentLevelModule = levelService.GoToNextModule();

//        List<AudioFragment> audioFragments = currentLevelModule.GetAudioFragments();

//        List<AudioClip> audioClips = audioFragments.ConvertAll(audioFragments => audioFragments.GetAudioClip());


//        if (currentLevelModule.GetLevelModuleType() == LevelModule.Type.QUIZ)
//        {
//            choiceController.SwitchLayout(ChoiceController.TextLayout.ThreeQuestions);
//        }

//        audioService.PlayAudio(audioClips, (currentIndex) => {


//            if (currentLevelModule.GetLevelModuleType() == LevelModule.Type.QUIZ)
//            {
//                choiceController.SwitchLayout(ChoiceController.TextLayout.ThreeQuestions);
//            }
//            else { 
         
//            uIManager.setLargeText(audioFragments[currentIndex].GetText());

//            if (audioFragments[currentIndex].GetTexture() != null)
//            {
//                uIManager.setImageTexture(audioFragments[currentIndex].GetTexture());
//            }
//            }
//        }, () =>
//        {
//            if (currentLevelModule != null && currentLevelModule.GetLevelModuleType() != LevelModule.Type.QUIZ)
//            {
//                play();
//            }
//            else
//            {
//                return;
//            }
//        });
//    }
//}
