using System;
using UnityEngine;
using UnityEngine.UI;
public class PauseManager : MonoBehaviour
{
    [SerializeField] private GameObject _pauseMenu;
    
    public static PauseManager Instance;
    [SerializeField] private Toggle _subtitlesToggle;
    
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
        }
        
        Instance = this;
    }

    public void ToggleMenu(bool shouldOpen)
    {
        _pauseMenu.SetActive(shouldOpen);
    }

    private void Start()
    {
        _subtitlesToggle.onValueChanged.AddListener(delegate {
            ToggleValueChanged(_subtitlesToggle.isOn);
        });
    }

    private void ToggleValueChanged(bool isOn)
    {
        GameManager.Instance.areSubtitlesActivated = isOn;
    }
}
