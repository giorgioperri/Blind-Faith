using StarterAssets;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [HideInInspector] public bool isPaused;
    public bool areSubtitlesActivated = true;
    
    [HideInInspector] public StarterAssetsInputs playerInput;
    public static GameManager Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
        }
        
        Instance = this;
    }

    private void Start()
    {
        playerInput = FindObjectOfType<StarterAssetsInputs>();
    }

    void Update()
    {
        //Pause functionality
        if (playerInput.pause)
        {
            isPaused = !isPaused;
            PauseManager.Instance.ToggleMenu(isPaused);
            if (isPaused)
            {
                VoiceManager.Instance.audioSource.Pause();
            }
            else
            {
                VoiceManager.Instance.audioSource.UnPause();
            }
            playerInput.pause = false;
        }
    }
}