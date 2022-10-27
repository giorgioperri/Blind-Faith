using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubtitlesTrigger : MonoBehaviour
{
    public AudioClip voiceLineObject;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player")) VoiceManager.Instance.Say(voiceLineObject);
    }
}
