using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[System.Serializable]
public class AudioService : MonoBehaviour
{
    [SerializeField]
    public UnityEvent<AudioSource> _onCurrentAudioFragementChanged;

    // TODO : Implement methods for playing audio

    /// <summary>
    /// Plays muliple audioSources after each other, calls onCurrentAudioFragementChanged for each audioSource,
    /// when done the onDone callback is called.
    /// </summary>
    // public void PlayAudio(List<AudioSource> audioSources, Action<AudioSource> onCurrentAudioFragementChanged = null, Action onDone = null)
    // {
    //     StartCoroutine(playAudio(audioSources, onCurrentAudioFragementChanged, onDone));
    // }

    private IEnumerator playAudio(List<AudioSource> audioSources, Action<AudioSource> onCurrentAudioFragementChanged, Action onDone)
    {
        foreach (AudioSource audioSource in audioSources)
        {
            // Play audioSource
            audioSource.Play();

            // Notify listeners
            if (onCurrentAudioFragementChanged != null) onCurrentAudioFragementChanged(audioSource);
            _onCurrentAudioFragementChanged.Invoke(audioSource);

            // Wait until audioSource stopped playing
            yield return new WaitWhile(() => audioSource.isPlaying);
        }

        // Notify done when played all audioSources
        if (onDone != null) onDone();
    }
}