using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New VoiceLine", menuName = "Assets/New VoiceLine")]
public class VoiceLineSO : ScriptableObject
{
    public AK.Wwise.Event wwiseEvent;
    public string subtitle;
    public float nextClipOffset = .5f;
}
