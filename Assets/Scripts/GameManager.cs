using System;
using StarterAssets;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [HideInInspector] public bool isPaused;
    public bool isBathingInLight;
    public bool isLookingAtAngel;
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

    private void FixedUpdate()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        Debug.DrawRay(ray.origin, ray.direction *= 100, Color.green);
        RaycastHit hit;   
        
        if (Physics.Raycast(ray, out hit, 100))
        {
            if(hit.collider.CompareTag("ChargingTarget"))
            {
                isLookingAtAngel = true;
            }
            else
            {
                isLookingAtAngel = false;
            }
        }
        else if (isLookingAtAngel)
        {
            //has exited target
            if (isBathingInLight)
            {
                
            }
            isLookingAtAngel = false;
        }
    }
}
