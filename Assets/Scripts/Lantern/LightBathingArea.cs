using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBathingArea : MonoBehaviour
{
    public static LightBathingArea Instance;
    private bool _shouldshowTooltip = true;

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

        if (_shouldshowTooltip)
        {
            TooltipManager.Instance.currentTooltip = TooltipTypes.LanternCharge;
            TooltipManager.Instance.ToggleTooltip("Hold the Left Mouse button while looking at the Fallen Angel to charge up your Lantern");
            _shouldshowTooltip = false;
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.isBathingInLight = false;
        }
    }
}
