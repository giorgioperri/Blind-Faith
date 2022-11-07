using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanternManager : MonoBehaviour
{
    public float lanternCharge = 0;
    public bool canPlayChargeSound = true;
    [SerializeField] private float _lanternDepletingTime = 60;
    [SerializeField] private float _lanternRechargingTime = 3;
    [SerializeField] private Material lightMaterial;
    private Transform _orb;
    private Vector3 _originalOrbScale;
    
    public static LanternManager Instance;

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
        lightMaterial.SetFloat("_Opacity", lanternCharge);
        _orb.localScale = _originalOrbScale * lanternCharge;
        
        if (GameManager.Instance.isBathingInLight)
        {
            if (GameManager.Instance.isLookingAtAngel && lanternCharge <= 1)
            {
                //recharge
                if (canPlayChargeSound)
                {
                    PlayerSoundController.Instance.LanternCharging.Post(PlayerSoundController.Instance.gameObject);
                    canPlayChargeSound = false;
                }
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
            if(PlayerSoundController.Instance.LanternDischarged != null) 
                PlayerSoundController.Instance.LanternDischarged.Post(PlayerSoundController.Instance.gameObject);
        }
    }
    
    private void OnApplicationQuit()
    {
        lightMaterial.SetFloat("_Opacity", 1);
    }
}
