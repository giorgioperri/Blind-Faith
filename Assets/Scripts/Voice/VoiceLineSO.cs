using UnityEngine;

[CreateAssetMenu(fileName = "New VoiceLine", menuName = "Assets/New VoiceLine")]
public class VoiceLineSO : ScriptableObject
{
    public AudioClip voiceClip;
    public string subtitle;
}
