using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubtitlesTrigger : MonoBehaviour
{
    public List<VoiceLineSO> voiceLineSequence;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player")) StartCoroutine(VoiceManager.Instance.SaySequence(voiceLineSequence));
    }
}
