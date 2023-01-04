using System;
using UnityEngine;
using UnityEngine.UI;
public class PauseManager : MonoBehaviour
{
    [SerializeField] private GameObject _pauseMenu;
    
    public static PauseManager Instance;
    
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

    public void Unpause()
    {
        _pauseMenu.SetActive(false);
    }

    private void ToggleValueChanged(bool isOn)
    {
        GameManager.Instance.areSubtitlesActivated = isOn;
    }
}
