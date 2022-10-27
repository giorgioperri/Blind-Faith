using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class VoiceManager : MonoBehaviour
{
    private AudioSource _audioSource;
    public static VoiceManager Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
        }
        
        Instance = this;
    }

    private void Start()
    {
        _audioSource = gameObject.AddComponent<AudioSource>();
    }

    public IEnumerator SaySequence(List<VoiceLineSO> voiceLineSequence)
    {
        if (_audioSource.isPlaying) _audioSource.Stop();
        foreach (var lineObject in voiceLineSequence)
        {
            SubtitlesUI.Instance.ActivateSubtitles();
            SubtitlesUI.Instance.SetSubtitle(lineObject.subtitle);
            _audioSource.PlayOneShot(lineObject.voiceClip);
            yield return new WaitForSecondsRealtime(lineObject.clipDuration + lineObject.nextClipOffset);
        }
        
        SubtitlesUI.Instance.DeactivateSubtitles();
    }
}
