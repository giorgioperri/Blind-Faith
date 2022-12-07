using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBathingArea : MonoBehaviour
{
    public static LightBathingArea Instance;
    private bool _canTriggerSocketEvent = true;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
        }
        
        Instance = this;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.isBathingInLight = true;
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.isBathingInLight = false;
            if(TooltipManager.Instance.currentTooltip == TooltipTypes.LanternCharge) TooltipManager.Instance.CloseTooltip();

            if (LanternManager.Instance.lanternCharge > .5f && _canTriggerSocketEvent)
            {
                GameManager.Instance.InitSocketEvent();
                _canTriggerSocketEvent = false;
            }
        }
    }
}
