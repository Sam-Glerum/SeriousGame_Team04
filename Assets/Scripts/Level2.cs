using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2 : MonoBehaviour
{

    [SerializeField]
    private ServiceLocator serviceLocator;

    // Start is called before the first frame update
    void Start()
    {

        LevelService levelService = this.serviceLocator.GetLevelService();
            
        var x = System.DateTime.Now;

        levelService.StartTime = x;
        

        //Debug.Log(currentLevelModule.GetLevelModuleType());
        //Debug.Log(currentLevelModule.GetDuration());
        //Debug.Log(currentLevelModule.GetAudioFragments());
        Debug.Log(this.serviceLocator.GetLevelService().CurrentLevel);

        LevelModule z = levelService.GoToNextModule();


        //List<AudioFragment> audioFragments = currentLevelModule.GetAudioFragments();
        //Debug.Log(audioFragments.Count + " XXX ");

        //Debug.Log(currentLevelModule);

        //if (currentLevelModule == null) Debug.Log("JA");




    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
