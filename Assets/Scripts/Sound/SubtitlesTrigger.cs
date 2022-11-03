using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubtitlesTrigger : MonoBehaviour
{
    public VoiceLineSequenceSO voiceLineSequence;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SoundManager.Instance.InitVoiceLineSequence(voiceLineSequence);
        }
    }
}
