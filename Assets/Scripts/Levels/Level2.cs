using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class Level2 : MonoBehaviour
{
    [SerializeField]
    private UIManager uIManager;

    [SerializeField]
    private ServiceLocator serviceLocator;

    [SerializeField]
    private AudioClip wrongSoundEffect;

    [SerializeField]
    private SceneLoader sceneLoader;

    private AudioService audioService;
    private LevelService levelService;
    // Start is called before the first frame update
    private Task endLevelWhenTimeIsUp;

    void Start()
    {
        audioService = serviceLocator.GetAudioService();
        levelService = serviceLocator.GetLevelService();

        startLevel();
    }

    private void startLevel()
    {
        uIManager.StartedLevel(levelService.GetAvaiableTimeInSeconds);
        int time = (int)levelService.GetAvaiableTimeInSeconds() * 1000;
        endLevelWhenTimeIsUp = Task.Delay(time).ContinueWith(t => levelEnded());
        play();
    }

    private void levelEnded()
    {
        uIManager.StoppedLevel();
        sceneLoader.LoadHomeScreen();
    }

    private void play()
    {
        LevelModule currentLevelModule = levelService.GoToNextModule();

        if (currentLevelModule == null)
        {
            levelEnded();
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

        audioService.PlayAudio(audioClips, (currentIndex) =>
        {
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

        uIManager.ShowQuestion(
            question,
          answers: answers.ConvertAll((answr) => answr.value),
          onClick: (string value) =>
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
                   audioService.PlayAudio(soundEffect);
               }
           }
            );
    }
}
