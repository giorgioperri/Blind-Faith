using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBathingArea : MonoBehaviour
{
    public bool _canTriggerSocketEvent = true;

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
                GameManager.Instance.canPlayHealthTooltip = true;
                _canTriggerSocketEvent = false;
            }
        }
    }
}
