using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubtitlesTrigger : MonoBehaviour
{
    public VoiceLineSequenceSO voiceLineSequence;
    public bool playOnAwake;

    private void Start()
    {
        if (playOnAwake)
        {
            PlayerSoundController.Instance.InitVoiceLineSequence(voiceLineSequence);
            GetComponent<BoxCollider>().enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerSoundController.Instance.InitVoiceLineSequence(voiceLineSequence);
            GetComponent<BoxCollider>().enabled = false;
        }
    }
}
