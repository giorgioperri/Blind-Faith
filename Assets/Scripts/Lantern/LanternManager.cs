using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanternManager : MonoBehaviour
{
    public float _lanternCharge = 1;
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
        lightMaterial.SetFloat("_Opacity", _lanternCharge);
        _orb.localScale = _originalOrbScale * _lanternCharge;
        
        if (GameManager.Instance.isBathingInLight)
        {
            if (GameManager.Instance.isLookingAtAngel && _lanternCharge <= 1)
            {
                //recharge
                _lanternCharge += Time.deltaTime / _lanternRechargingTime;
            }
            return;
        }
        
        if (_lanternCharge >= 0)
        {
            //deplete
            _lanternCharge -= Time.deltaTime / _lanternDepletingTime;
        }

        if (_lanternCharge <= 0)
        {
            if(SoundManager.Instance.LanternDischarged != null) 
                SoundManager.Instance.LanternDischarged.Post(SoundManager.Instance.gameObject);
        }
    }
    
    private void OnApplicationQuit()
    {
        lightMaterial.SetFloat("_Opacity", 1);
    }
}
