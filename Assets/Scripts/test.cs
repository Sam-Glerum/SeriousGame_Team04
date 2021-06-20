using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    [SerializeField]
    private UIManager uIManager;

    private AudioService audioService;
    private LevelService levelService;

    // Start is called before the first frame update
    void Start()
    {
        this.audioService = new AudioService();
        this.levelService = new LevelService();

        this.uIManager.setLargeText("hopla");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
