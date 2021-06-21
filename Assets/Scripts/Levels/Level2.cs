using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2 : MonoBehaviour
{
    [SerializeField]
    private UIManager uIManager;

    [SerializeField]
    private ServiceLocator serviceLocator;

    [SerializeField]
    private AudioClip wrongSoundEffect;
    //[SerializeField]
    //private ChoiceController choiceController;

    private AudioService audioService;
    private LevelService levelService;
    // Start is called before the first frame update

    private Action<string> handleQuestionSelectedCallback;


    void Start()
    {
        audioService = serviceLocator.GetAudioService();
        levelService = serviceLocator.GetLevelService();

        play();
    }

    private void play()
    {
        LevelModule currentLevelModule = levelService.GoToNextModule();

        if (currentLevelModule == null)
        {
            // Maybe do something?
            return;
        }

        Action onDone = () =>
        {
            play();
        };

        switch (currentLevelModule.GetLevelModuleType())
        {
            case LevelModule.Type.QUIZ:
                handleQuestionModule(currentLevelModule, onDone);
                break;
            case LevelModule.Type.AUDIO:
                handleAudioLevelModule(currentLevelModule, onDone);
                break;
        }
    }

    private void handleAudioLevelModule(LevelModule levelModule, Action onDone)
    {
        List<AudioFragment> audioFragments = levelModule.GetAudioFragments();

        List<AudioClip> audioClips = audioFragments.ConvertAll(audioFragments => audioFragments.GetAudioClip());

        audioService.PlayAudio(audioClips, (currentIndex) => {
            uIManager.setLargeText(audioFragments[currentIndex].GetText());

            if (audioFragments[currentIndex].GetTexture() != null)
            {
                uIManager.setImageTexture(audioFragments[currentIndex].GetTexture());
            }
        }, onDone);
    }

    private void handleQuestionModule(LevelModule levelModule, Action onDone)
    {
        // update ui shwo question and possible answers
        string question = levelModule.GetQuestion();
        var answers = levelModule.GetAnswers();

        uIManager.ShowQuestion(question, answers.ConvertAll((answr) => answr.value));

        // set callback
        handleQuestionSelectedCallback = (string value) =>
         {
             // Check if answer is correct
             LevelModule.Answer? foundAnswer = answers.Find((answer) => answer.value == value);
             bool isCorrectlyAnswred = foundAnswer?.isValidAnswer ?? false;

             // Update ui properly
             uIManager.ShowAnswer(isCorrectlyAnswred);

             if (isCorrectlyAnswred)
             {
                 onDone();
             }
             else
             {
                 List<AudioClip> soundEffect = new List<AudioClip>();
                 soundEffect.Add(wrongSoundEffect);
                 audioService.PlayAudio( soundEffect);
             }
        };
    }

    public void handleQuestionSelected(string value) {
        handleQuestionSelectedCallback(value);
    }
}
