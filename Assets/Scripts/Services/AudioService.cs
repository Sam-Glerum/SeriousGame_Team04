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
    UnityEvent<AudioClip> _onCurrentAudioClipChanged;

    /// <summary>
    /// Plays muliple audioClips after each other, calls onCurrentAudioClipChanged for each audioClip,
    /// when done the onDone callback is called.
    /// </summary>
    public void PlayAudio(List<AudioClip> audioClips, Action<int> onCurrentAudioClipChanged = null, Action onDone = null)
    {
        StartCoroutine(playAudio(audioClips, onCurrentAudioClipChanged, onDone));
    }

    private IEnumerator playAudio(List<AudioClip> audioClips, Action<int> onCurrentAudioClipChanged, Action onDone)
    {
        AudioSource audio = GetComponent<AudioSource>();

        for (int i = 0; i < audioClips.Count; i++)
        {
            var audioClip = audioClips[i];

            // Play audioClip
            audio.Play();
            audio.clip = audioClip;

            // Notify listeners
            if (onCurrentAudioClipChanged != null) onCurrentAudioClipChanged(i);
            _onCurrentAudioClipChanged.Invoke(audioClip);

            // Wait until audioSource finished playing clip
            yield return new WaitForSeconds(audio.clip.length); 
        }

        //foreach (AudioClip audioClip in audioClips)
        //{
        //    // Play audioClip
        //    audio.Play();
        //    audio.clip = audioClip;

        //    // Notify listeners
        //    if (onCurrentAudioClipChanged != null) onCurrentAudioClipChanged(audioClip);
        //    _onCurrentAudioClipChanged.Invoke(audioClip);

        //    // Wait until audioSource finished playing clip
        //    yield return new WaitForSeconds(audio.clip.length);
        //}

        // Notify done when played all audioClips
        if (onDone != null) onDone();
    }
}
