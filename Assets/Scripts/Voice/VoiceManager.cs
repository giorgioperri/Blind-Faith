using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class VoiceManager : MonoBehaviour
{
    public static VoiceManager Instance;
    
    private int _currentSequenceIndex = 0;
    private VoiceLineSequenceSO _currentVoiceLineSequence;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
        }
        
        Instance = this;
    }

    public void InitVoiceLineSequence(VoiceLineSequenceSO voiceLineSequence)
    {
        StopAllCoroutines();
        _currentSequenceIndex = 0;
        _currentVoiceLineSequence = voiceLineSequence;

        if(GameManager.Instance.areSubtitlesActivated) SubtitlesUI.Instance.ActivateSubtitles();
        SubtitlesUI.Instance.SetSubtitle(voiceLineSequence.sequenceLines[_currentSequenceIndex].subtitle);
        voiceLineSequence.sequenceLines[_currentSequenceIndex].wwiseEvent.Post(gameObject, (uint)AkCallbackType.AK_EndOfEvent, WwiseEventEnd);
    }
    
    public void WwiseEventEnd(object in_cookie, AkCallbackType in_type, object in_info)
    {
        if (in_type == AkCallbackType.AK_EndOfEvent) {
            SubtitlesUI.Instance.DeactivateSubtitles();
            _currentSequenceIndex++;

            if (_currentSequenceIndex + 1 <= _currentVoiceLineSequence.sequenceLines.Count)
            {
                StartCoroutine(WaitForOffset());
            }
            else
            {
                _currentVoiceLineSequence = null;
            }
        }
    }

    private IEnumerator WaitForOffset()
    {
        yield return new WaitForSecondsRealtime(_currentVoiceLineSequence.sequenceLines[_currentSequenceIndex -1 ].nextClipOffset);
        yield return new WaitUntil(() => !GameManager.Instance.isPaused);
        NextLine(_currentVoiceLineSequence.sequenceLines[_currentSequenceIndex]);
    }

    public void NextLine(VoiceLineSO voiceLine)
    {
        if(GameManager.Instance.areSubtitlesActivated) SubtitlesUI.Instance.ActivateSubtitles();
        SubtitlesUI.Instance.SetSubtitle(voiceLine.subtitle);
        voiceLine.wwiseEvent.Post(gameObject, (uint)AkCallbackType.AK_EndOfEvent, WwiseEventEnd);
    }
}
