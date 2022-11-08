using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New VoiceLineSequence", menuName = "Assets/New VoiceLineSequence")]
public class VoiceLineSequenceSO : ScriptableObject
{
    public List<VoiceLineSO> sequenceLines;
    public AK.Wwise.Event setSwitchEvent;
}
