using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanternManager : MonoBehaviour
{
    public float _lanternCharge = 0;
    [HideInInspector] public float currentLanternDepleteTime = 0;
    [HideInInspector] public float currentLanternRechargeTime = 0;
    [SerializeField] private float _lanternDepletingTime = 60;
    [SerializeField] private float _lanternRechargingTime = 3;
    [SerializeField] private Material lightMaterial;
    
    public static LanternManager Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
        }
        
        Instance = this;
    }

    private void Update()
    {
        lightMaterial.SetFloat("_Opacity", _lanternCharge);
        
        if (GameManager.Instance.isBathingInLight)
        {
            if (GameManager.Instance.isLookingAtAngel && _lanternCharge <= 1)
            {
                currentLanternRechargeTime += Time.deltaTime;
                _lanternCharge = Mathf.Lerp (0, 1, Mathf.InverseLerp (0, _lanternRechargingTime, currentLanternRechargeTime));
            }
            return;
        }
        
        if (_lanternCharge >= 0 && currentLanternDepleteTime <= _lanternDepletingTime)
        {
            currentLanternDepleteTime += Time.deltaTime;
            _lanternCharge = Mathf.Lerp (1, 0, Mathf.InverseLerp (0, _lanternDepletingTime, currentLanternDepleteTime));
        }
    }
    

    private void OnApplicationQuit()
    {
        lightMaterial.SetFloat("_Opacity", 1);
    }
}
