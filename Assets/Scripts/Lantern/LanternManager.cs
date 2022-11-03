using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanternManager : MonoBehaviour
{
    private float _lanternCharge = 0;
    [SerializeField] private float _lanternDepletingTime = 180;
    private float _currentLanternTime = 0;
    [SerializeField] private Material lightMaterial;

    private void Update()
    {
        if (_lanternCharge >= 0 && _currentLanternTime <= _lanternDepletingTime)
        {
            _currentLanternTime += Time.deltaTime;
            
            _lanternCharge = Mathf.Lerp (1, 0, Mathf.InverseLerp (0, _lanternDepletingTime, _currentLanternTime));

            lightMaterial.SetFloat("_Opacity", _lanternCharge);
        }
    }

    private void OnApplicationQuit()
    {
        lightMaterial.SetFloat("_Opacity", 1);
    }
}
