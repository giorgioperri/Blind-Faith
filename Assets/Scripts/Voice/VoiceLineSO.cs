using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New VoiceLine", menuName = "Assets/New VoiceLine")]
public class VoiceLineSO : ScriptableObject
{
    public AudioClip voiceClip;
    public string subtitle;
    public float clipDuration = 0;
    public float nextClipOffset = .5f;
    public AK.Wwise.Switch wiseSwitch;

    private void Awake()
    {
        if (voiceClip != null)
        {
            clipDuration = voiceClip.length;
        }
    }
}
