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
        LevelModule currentLevelModule = this.serviceLocator.GetLevelService().GetCurrentModule();
        Debug.Log(currentLevelModule);

        if (currentLevelModule == null) Debug.Log("JA");

        Debug.Log(currentLevelModule.GetLevelModuleType());
        Debug.Log(currentLevelModule.GetDuration());
        Debug.Log(currentLevelModule.GetAudioFragments());

        Debug.Log(this.serviceLocator.GetLevelService().CurrentLevel);


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
