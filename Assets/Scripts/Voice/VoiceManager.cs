using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class VoiceManager : MonoBehaviour
{
    private AudioSource _audioSource;
    public static VoiceManager Instance;

    private bool _isPlaying;

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

    public IEnumerator SaySequence(VoiceLineSequenceSO voiceLineSequence)
    {
        _audioSource.Stop();
        
        foreach (var lineObject in voiceLineSequence.sequenceLines)
        {
            if (_audioSource.isPlaying) break;
            if(GameManager.Instance.areSubtitlesActivated) SubtitlesUI.Instance.ActivateSubtitles();
            SubtitlesUI.Instance.SetSubtitle(lineObject.subtitle);
            _audioSource.PlayOneShot(lineObject.voiceClip);
            yield return new WaitForSecondsRealtime(lineObject.clipDuration + lineObject.nextClipOffset);
        }
        
        SubtitlesUI.Instance.DeactivateSubtitles();
    }
}
