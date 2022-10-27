using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SubtitlesUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _subtitleText = default;
    public static SubtitlesUI Instance;
    
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
        }
        
        Instance = this;
    }

    public void SetSubtitle(string subtitle)
    {
        _subtitleText.text = subtitle;
    }
}
