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

    private bool _canPlayChargeEvent = true;

    public AK.Wwise.Event PauseVA;
    public AK.Wwise.Event ResumeVA;

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
                PauseVA.Post(SoundManager.Instance.gameObject);
            }
            else
            {
                ResumeVA.Post(SoundManager.Instance.gameObject);
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
                if (_canPlayChargeEvent)
                {
                    _canPlayChargeEvent = false;
                }
                isLookingAtAngel = true;
            }
            else
            {
                isLookingAtAngel = false;
            }
        }
        else if (isLookingAtAngel)
        {
            //has exited charging target
            _canPlayChargeEvent = true;
            isLookingAtAngel = false;
        }
    }
}
