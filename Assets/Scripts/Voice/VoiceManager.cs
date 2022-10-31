using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class VoiceManager : MonoBehaviour
{
    [HideInInspector] public AudioSource audioSource;
    public static VoiceManager Instance;

    private bool _isPlaying;
    
    private int _currentSequenceIndex = 0;
    private VoiceLineSequenceSO _currentVoiceLineSequence;
    private float _currentVoiceLineTime = 0;

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
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    private void Update()
    {
        if (GameManager.Instance.isPaused || !_currentVoiceLineSequence || _currentSequenceIndex + 1 > _currentVoiceLineSequence.sequenceLines.Count) return;

        _currentVoiceLineTime += Time.deltaTime;

        if (_currentVoiceLineTime >= _currentVoiceLineSequence.sequenceLines[_currentSequenceIndex].clipDuration 
            + _currentVoiceLineSequence.sequenceLines[_currentSequenceIndex].nextClipOffset)
        {
            SubtitlesUI.Instance.DeactivateSubtitles();
            _currentVoiceLineTime = 0;
            _currentSequenceIndex++;

            if (_currentSequenceIndex + 1 <= _currentVoiceLineSequence.sequenceLines.Count)
            {
                NextLine(_currentVoiceLineSequence.sequenceLines[_currentSequenceIndex]);
            }
            else
            {
                _currentVoiceLineSequence = null;
            }
        }
    }

    public void InitVoiceLineSequence(VoiceLineSequenceSO voiceLineSequence)
    {
        audioSource.Stop();
        _currentSequenceIndex = 0;
        _currentVoiceLineTime = 0;
        _currentVoiceLineSequence = voiceLineSequence;

        if(GameManager.Instance.areSubtitlesActivated) SubtitlesUI.Instance.ActivateSubtitles();
        SubtitlesUI.Instance.SetSubtitle(voiceLineSequence.sequenceLines[_currentSequenceIndex].subtitle);
        audioSource.PlayOneShot(voiceLineSequence.sequenceLines[_currentSequenceIndex].voiceClip);
    }

    public void NextLine(VoiceLineSO voiceLine)
    {
        if(GameManager.Instance.areSubtitlesActivated) SubtitlesUI.Instance.ActivateSubtitles();
        SubtitlesUI.Instance.SetSubtitle(voiceLine.subtitle);
        audioSource.PlayOneShot(voiceLine.voiceClip);
    }
}
