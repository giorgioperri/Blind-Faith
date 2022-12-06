using System;
using StarterAssets;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    [HideInInspector] public bool isPaused;
    public bool isBathingInLight;
    public bool isLookingAtAngel;
    public bool areSubtitlesActivated = true;

    public bool areMirrorsStep;
    public bool isInteractingWithMirror;

    //health System implementation
    public float health = 100; 

    [HideInInspector] public StarterAssetsInputs playerInput;
    
    public static GameManager Instance;

    public AK.Wwise.Event PauseVA;
    public AK.Wwise.Event ResumeVA;

    private float wTime;

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
        TooltipManager.Instance.ToggleTooltip("Use the WASD keys to move");
        TooltipManager.Instance.currentTooltip = TooltipTypes.WASDMove;
    }

    void Update()
    {
        if (Keyboard.current.wKey.isPressed && wTime < 1.2f)
        {
            wTime += Time.deltaTime;
        }

        if (Keyboard.current.wKey.wasReleasedThisFrame)
        {
            wTime = 0;
        }

        if (wTime > 1.2f && TooltipManager.Instance.currentTooltip == TooltipTypes.WASDMove)
        {
            TooltipManager.Instance.CloseTooltip();
        }
        
        if (playerInput.pause)
        {
            isPaused = !isPaused;
            PauseManager.Instance.ToggleMenu(isPaused);
            if (isPaused)
            {
                PauseVA.Post(PlayerSoundController.Instance.gameObject);
            }
            else
            {
                ResumeVA.Post(PlayerSoundController.Instance.gameObject);
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
            //has exited charging target
            LanternManager.Instance.canPlayChargeSound = true;
            isLookingAtAngel = false;
        }
    }
}
