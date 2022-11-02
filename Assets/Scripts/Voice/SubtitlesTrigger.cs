using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubtitlesTrigger : MonoBehaviour
{
    public VoiceLineSequenceSO voiceLineSequence;
    public AK.Wwise.Event dialogue;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            WwiseDialogue();
        }
    }   
    
    public void WwiseDialogue() {
        dialogue.Post(gameObject, (uint)AkCallbackType.AK_EndOfEvent, WwiseLastDialogueEnd);
    }
    public void WwiseLastDialogueEnd(object in_cookie, AkCallbackType in_type, object in_info) {
        if (in_type == AkCallbackType.AK_EndOfEvent) {
            /*AkMarkerCallbackInfo info = (AkMarkerCallbackInfo)in_info;
            print(info.strLabel); // This prints the marker name*/
            Debug.Log("end");
        }
    }
    
}
