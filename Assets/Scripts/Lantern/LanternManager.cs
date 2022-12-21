using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using StarterAssets;
using UnityEngine;
using UnityEngine.InputSystem;

public class LanternManager : MonoBehaviour
{
    public float lanternCharge = 0;
    public bool canPlayChargeSound = true;
    public bool canPlayDischargeSound;
    public bool isInsideSocket;
    public bool lanternIsRaised;
    [SerializeField] private float _lanternDepletingTime = 60;
    [SerializeField] private float _lanternRechargingTime = 3;
    [SerializeField] private Material lightMaterial;
    private Transform _orb;
    private Vector3 _originalOrbScale;
    
    public static LanternManager Instance;
    [SerializeField] private CinemachineVirtualCamera playerVC;

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
        _orb = transform.Find("Light");
        _originalOrbScale = _orb.localScale;
    }

    private void Update()
    {
        lanternCharge = Mathf.Clamp01(lanternCharge);
        lightMaterial.SetFloat("_Opacity", lanternCharge);
        _orb.localScale = _originalOrbScale * lanternCharge;
        
        if (lanternCharge >= .8f && TooltipManager.Instance.currentTooltip == TooltipTypes.LanternCharge)
        {
            TooltipManager.Instance.currentTooltip = TooltipTypes.None;
            TooltipManager.Instance.CloseTooltip();
        }

        if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            playerVC.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0;
            playerVC.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = 0;
        }
        
        if (GameManager.Instance.isBathingInLight && lanternIsRaised && !isInsideSocket)
        {
            if (GameManager.Instance.isLookingAtAngel && lanternCharge <= 1 && Mouse.current.leftButton.isPressed)
            {
                playerVC.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = .1f;
                playerVC.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = 15f;
                //recharge
                if (canPlayChargeSound && lanternCharge == 0)
                {
                    PlayerSoundController.Instance.LanternCharging.Post(PlayerSoundController.Instance.gameObject);
                    canPlayChargeSound = false;
                }
                canPlayDischargeSound = true;
                lanternCharge += Time.deltaTime / _lanternRechargingTime;
            }
            return;
        }

        if (lanternCharge >= 0)
        {
            //deplete
            lanternCharge -= Time.deltaTime / _lanternDepletingTime;
        }

        if (lanternCharge <= 0)
        {
            FirstPersonController.Instance.currentInteractable = null;
            GameManager.Instance.isInteractingWithMirror = false;
        }

        if (lanternCharge <= 0 && canPlayDischargeSound)
        {
            canPlayDischargeSound = false;
            if(PlayerSoundController.Instance.LanternDischarged != null) 
                PlayerSoundController.Instance.LanternDischarged.Post(PlayerSoundController.Instance.gameObject);
        }
    }
    
    private void OnApplicationQuit()
    {
        lightMaterial.SetFloat("_Opacity", 1);
    }
}
