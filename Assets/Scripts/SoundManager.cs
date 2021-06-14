using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    [SerializeField]
    public List<UIObject> objects;

    public Text UIText;
    public GameObject UI;
    public GameObject Game;

    // Start is called before the first frame update

    void Start()
    {
        Game.SetActive(false);
        StartCoroutine(example(
            objects,
        updateText,
        onDone));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void updateText(UIObject audioFragment) 
        {
            UIText.text = audioFragment.text;
            UIText.fontSize = audioFragment.fontSize;
        }

    void onDone()
    {
        UI.SetActive(false);
        Game.SetActive(true);
    }

IEnumerator example(List<UIObject> objects, Action<UIObject> onCurrentAudioFragementChanged, Action onDone)
    {
        for (int index = 0; index < objects.Count; index++) {
            var audioFragment = objects[index];
            audioFragment.audioSource.Play();
            onCurrentAudioFragementChanged(audioFragment);
            yield return new WaitWhile(() => audioFragment.audioSource.isPlaying);
        }

        onDone();
    }
}
