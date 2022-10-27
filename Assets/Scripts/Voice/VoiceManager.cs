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

    public void Say(VoiceLineSO voiceLineObject)
    {
        if (_audioSource.isPlaying) _audioSource.Stop();
        
        _audioSource.PlayOneShot(voiceLineObject.voiceClip);
        
        SubtitlesUI.Instance.SetSubtitle(voiceLineObject.subtitle);
    }
}
